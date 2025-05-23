const Invoice = require('../models/Invoice');
const Job = require('../models/Job');
const Service = require('../models/Service');
const InventoryItem = require('../models/InventoryItem');
const mongoose = require('mongoose');
const AutoIncrement = require('mongoose-sequence')(mongoose); // If using this for invoiceNumber

// If not using mongoose-sequence for invoice numbers, you might implement a counter model
// For now, we assume mongoose-sequence is set up on the Invoice model or we manually handle it.

// Helper function to validate ObjectId
const isValidObjectId = (id) => mongoose.Types.ObjectId.isValid(id);

// Function to generate next invoice number (example if not using mongoose-sequence directly on model)
// This is a simplified example. A robust solution would use a separate counter collection in the DB.
let lastInvoiceNum = 0; // In-memory counter, NOT FOR PRODUCTION
async function getNextInvoiceNumber() {
    // In a real app, you'd fetch and increment a counter from the DB.
    // For example, using a dedicated 'Counter' model.
    // Or, if mongoose-sequence is configured on the Invoice model, this function is not needed
    // as the number is generated pre-save.
    lastInvoiceNum++;
    return `INV-${String(lastInvoiceNum).padStart(5, '0')}`;
}


exports.createInvoiceFromJob = async (req, res) => {
  try {
    const { jobId } = req.body;

    if (!jobId || !isValidObjectId(jobId)) {
      return res.status(400).json({ message: 'Valid Job ID is required.' });
    }

    const job = await Job.findById(jobId)
      .populate('services')
      .populate('parts.item');

    if (!job || job.isDeleted) {
      return res.status(404).json({ message: 'Job not found or has been deleted.' });
    }

    if (job.status !== 'Completed') {
      return res.status(400).json({ message: 'Job must be marked as "Completed" before an invoice can be generated.' });
    }
    
    // Check if an invoice already exists for this job
    const existingInvoice = await Invoice.findOne({ job: jobId, isDeleted: false });
    if (existingInvoice) {
        return res.status(400).json({ message: 'An invoice already exists for this job.', invoiceId: existingInvoice._id });
    }


    const lineItems = [];
    let totalAmount = 0;

    // Process services
    if (job.services && job.services.length > 0) {
      for (const service of job.services) {
        if (!service || service.isDeleted) {
            console.warn(`Service with ID ${service._id} linked to job ${jobId} is missing or deleted. Skipping.`);
            continue;
        }
        lineItems.push({
          description: service.name,
          quantity: 1,
          unitPrice: service.price,
          totalPrice: service.price,
        });
        totalAmount += service.price;
      }
    }

    // Process parts
    if (job.parts && job.parts.length > 0) {
      for (const partUsage of job.parts) {
        if (!partUsage.item || partUsage.item.isDeleted) {
            console.warn(`InventoryItem with ID ${partUsage.item._id} used in job ${jobId} is missing or deleted. Skipping.`);
            continue;
        }
        const partTotal = partUsage.quantity * partUsage.item.price;
        lineItems.push({
          description: partUsage.item.name,
          quantity: partUsage.quantity,
          unitPrice: partUsage.item.price,
          totalPrice: partTotal,
        });
        totalAmount += partTotal;
      }
    }
    
    // Update actualCost on the job if it's different or not set
    if (job.actualCost !== totalAmount) {
        job.actualCost = totalAmount;
    }

    const invoiceNumber = await getNextInvoiceNumber(); // Or rely on mongoose-sequence

    const newInvoice = new Invoice({
      job: jobId,
      invoiceNumber, // This will be handled by mongoose-sequence if plugin is active on schema
      lineItems,
      totalAmount,
      paymentStatus: 'Pending',
    });

    // If using mongoose-sequence and NOT calling getNextInvoiceNumber() manually:
    // The `invoiceNumber` field would be populated by the pre-save hook from the plugin.
    // If manually generating like above, ensure your Invoice model doesn't also try to generate it.
    // For this example, we'll assume `invoiceNumber` is manually set if `mongoose-sequence` is not directly
    // responsible for the `invoiceNumber` field but rather an internal sequence number.
    // If `invoiceSchema.plugin(AutoIncrement, { inc_field: 'invoiceNumber', ...})` was used,
    // then `invoiceNumber` would be automatically an integer and you might format it in a getter or pre-save.

    await newInvoice.save();
    job.status = 'Invoiced';
    await job.save();

    const populatedInvoice = await Invoice.findById(newInvoice._id)
        .populate({
            path: 'job',
            populate: [
                { path: 'customer', select: 'name email phone' },
                { path: 'vehicle', select: 'make model licensePlate vin' }
            ]
        });

    res.status(201).json({ message: 'Invoice created successfully', invoice: populatedInvoice });
  } catch (error) {
    console.error('Error creating invoice:', error);
    if (error.name === 'ValidationError') {
      return res.status(400).json({ message: 'Validation error', errors: error.errors });
    }
    res.status(500).json({ message: 'Server error while creating invoice.' });
  }
};

exports.getAllInvoices = async (req, res) => {
  try {
    const { customerId, paymentStatus, startDate, endDate, page = 1, limit = 10 } = req.query;
    const query = { isDeleted: false };

    if (paymentStatus) query.paymentStatus = paymentStatus;
    if (startDate || endDate) {
      query.invoiceDate = {};
      if (startDate) query.invoiceDate.$gte = new Date(startDate);
      if (endDate) query.invoiceDate.$lte = new Date(endDate);
    }

    // For filtering by customerId, we need to find jobs by that customer first
    let jobIds = [];
    if (customerId) {
      if (!isValidObjectId(customerId)) return res.status(400).json({ message: "Invalid customer ID format."});
      const jobsByCustomer = await Job.find({ customer: customerId, isDeleted: false }).select('_id');
      jobIds = jobsByCustomer.map(job => job._id);
      if (jobIds.length === 0) { // No jobs for this customer, so no invoices
        return res.status(200).json({ invoices: [], totalPages: 0, currentPage: 1, totalInvoices: 0 });
      }
      query.job = { $in: jobIds };
    }

    const invoices = await Invoice.find(query)
      .populate({
        path: 'job',
        select: 'description customer vehicle jobDate', // Select specific fields from Job
        populate: [ // Nested population
          { path: 'customer', select: 'name phone' },
          { path: 'vehicle', select: 'make model licensePlate' }
        ]
      })
      .limit(limit * 1)
      .skip((page - 1) * limit)
      .sort({ invoiceDate: -1 });

    const count = await Invoice.countDocuments(query);

    res.status(200).json({
      invoices,
      totalPages: Math.ceil(count / limit),
      currentPage: parseInt(page),
      totalInvoices: count,
    });
  } catch (error) {
    console.error('Error fetching invoices:', error);
    res.status(500).json({ message: 'Server error while fetching invoices.' });
  }
};

exports.getInvoiceById = async (req, res) => {
  try {
    const { id } = req.params;
     if (!isValidObjectId(id)) {
        return res.status(400).json({ message: 'Invalid Invoice ID format.' });
    }

    const invoice = await Invoice.findOne({ _id: id, isDeleted: false })
      .populate({
        path: 'job',
        populate: [
          { path: 'customer', select: 'name email phone address' },
          { path: 'vehicle', select: 'make model year vin licensePlate' },
          { path: 'assignedMechanic', select: 'username role'},
          { path: 'services', select: 'name price description' },
          { path: 'parts.item', select: 'name price sku description' }
        ]
      });

    if (!invoice) {
      return res.status(404).json({ message: 'Invoice not found or has been deleted.' });
    }
    res.status(200).json(invoice);
  } catch (error) {
    console.error('Error fetching invoice by ID:', error);
    res.status(500).json({ message: 'Server error while fetching invoice.' });
  }
};

exports.updateInvoiceStatus = async (req, res) => {
  try {
    const { id } = req.params;
    const { paymentStatus } = req.body;

    if (!isValidObjectId(id)) {
        return res.status(400).json({ message: 'Invalid Invoice ID format.' });
    }
    if (!paymentStatus) {
      return res.status(400).json({ message: 'Payment status is required.' });
    }
    // Optional: Validate if paymentStatus is one of the enum values
    const allowedStatuses = Invoice.schema.path('paymentStatus').enumValues;
    if (!allowedStatuses.includes(paymentStatus)) {
        return res.status(400).json({ message: `Invalid payment status. Allowed values are: ${allowedStatuses.join(', ')}` });
    }


    const invoice = await Invoice.findOne({ _id: id, isDeleted: false });
    if (!invoice) {
      return res.status(404).json({ message: 'Invoice not found or has been deleted.' });
    }
    
    if (invoice.paymentStatus === 'Deleted' || invoice.paymentStatus === 'Void') {
        return res.status(400).json({ message: `Invoice is already ${invoice.paymentStatus} and its status cannot be changed.` });
    }


    invoice.paymentStatus = paymentStatus;
    await invoice.save();
    
    const populatedInvoice = await Invoice.findById(invoice._id)
        .populate({
            path: 'job',
            select: 'description customer vehicle',
            populate: [
                { path: 'customer', select: 'name phone' },
                { path: 'vehicle', select: 'make model' }
            ]
        });

    res.status(200).json({ message: 'Invoice status updated successfully', invoice: populatedInvoice });
  } catch (error) {
    console.error('Error updating invoice status:', error);
     if (error.name === 'ValidationError') {
      return res.status(400).json({ message: 'Validation error', errors: error.errors });
    }
    res.status(500).json({ message: 'Server error while updating invoice status.' });
  }
};

exports.deleteInvoice = async (req, res) => {
  try {
    const { id } = req.params;
    if (!isValidObjectId(id)) {
        return res.status(400).json({ message: 'Invalid Invoice ID format.' });
    }

    const invoice = await Invoice.findById(id);
    if (!invoice || invoice.isDeleted) {
      return res.status(404).json({ message: 'Invoice not found or already deleted.' });
    }

    // Soft delete: Mark as 'Void' or 'Deleted'
    invoice.isDeleted = true;
    invoice.paymentStatus = 'Deleted'; // Or 'Void'
    await invoice.save();

    res.status(200).json({ message: 'Invoice deleted (soft delete) successfully.' });
  } catch (error) {
    console.error('Error deleting invoice:', error);
    res.status(500).json({ message: 'Server error while deleting invoice.' });
  }
};
