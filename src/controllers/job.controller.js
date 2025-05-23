const Job = require('../models/Job');
const Customer = require('../models/Customer');
const Vehicle = require('../models/Vehicle');
const InventoryItem = require('../models/InventoryItem'); // For validating parts
const Service = require('../models/Service'); // For validating services
const mongoose = require('mongoose');

// Helper function to validate ObjectId
const isValidObjectId = (id) => mongoose.Types.ObjectId.isValid(id);

// Create a new job
exports.createJob = async (req, res) => {
  try {
    const {
      customerId,
      vehicleId,
      description,
      jobDate,
      services,
      parts,
      assignedMechanic,
      status,
      estimatedCost,
    } = req.body;

    // Basic Validations
    if (!customerId || !vehicleId || !description) {
      return res.status(400).json({ message: 'Customer, vehicle, and description are required.' });
    }
    if (!isValidObjectId(customerId) || !isValidObjectId(vehicleId)) {
      return res.status(400).json({ message: 'Invalid Customer or Vehicle ID format.' });
    }
    if (assignedMechanic && !isValidObjectId(assignedMechanic)) {
      return res.status(400).json({ message: 'Invalid Assigned Mechanic ID format.' });
    }

    // Check existence of customer and vehicle
    const customer = await Customer.findById(customerId);
    if (!customer || customer.isDeleted) {
      return res.status(404).json({ message: 'Customer not found.' });
    }
    const vehicle = await Vehicle.findById(vehicleId);
    if (!vehicle || vehicle.isDeleted) {
      return res.status(404).json({ message: 'Vehicle not found.' });
    }
    // Ensure vehicle belongs to the customer
    if (vehicle.customer.toString() !== customerId) {
        return res.status(400).json({ message: 'Vehicle does not belong to the specified customer.' });
    }

    // Validate services if provided
    if (services && services.length > 0) {
      for (const serviceId of services) {
        if (!isValidObjectId(serviceId)) {
          return res.status(400).json({ message: `Invalid service ID format: ${serviceId}` });
        }
        const serviceExists = await Service.findById(serviceId);
        if (!serviceExists || serviceExists.isDeleted) {
          return res.status(404).json({ message: `Service not found: ${serviceId}` });
        }
      }
    }

    // Validate parts if provided
    if (parts && parts.length > 0) {
      for (const part of parts) {
        if (!part.item || !isValidObjectId(part.item) || !part.quantity || part.quantity < 1) {
          return res.status(400).json({ message: 'Invalid part data. Each part must have a valid item ID and quantity >= 1.' });
        }
        const inventoryItemExists = await InventoryItem.findById(part.item);
        if (!inventoryItemExists || inventoryItemExists.isDeleted) {
          return res.status(404).json({ message: `Inventory item not found: ${part.item}` });
        }
      }
    }


    const newJob = new Job({
      customer: customerId,
      vehicle: vehicleId,
      description,
      jobDate: jobDate || Date.now(),
      services: services || [],
      parts: parts || [],
      assignedMechanic: assignedMechanic || null,
      status: status || 'Pending',
      estimatedCost,
    });

    await newJob.save();

    // Populate for response
    const populatedJob = await Job.findById(newJob._id)
      .populate('customer', 'name phone email')
      .populate('vehicle', 'make model licensePlate vin')
      .populate('assignedMechanic', 'username role')
      .populate('services', 'name price')
      .populate('parts.item', 'name price sku');

    res.status(201).json({ message: 'Job created successfully', job: populatedJob });
  } catch (error) {
    console.error('Error creating job:', error);
    if (error.name === 'ValidationError') {
      return res.status(400).json({ message: 'Validation error', errors: error.errors });
    }
    res.status(500).json({ message: 'Server error while creating job.' });
  }
};

// Get all jobs with filtering and pagination
exports.getAllJobs = async (req, res) => {
  try {
    const { customerId, vehicleId, status, assignedMechanic, startDate, endDate, page = 1, limit = 10 } = req.query;
    const query = { isDeleted: false };

    if (customerId) query.customer = customerId;
    if (vehicleId) query.vehicle = vehicleId;
    if (status) query.status = status;
    if (assignedMechanic) query.assignedMechanic = assignedMechanic;
    if (startDate || endDate) {
      query.jobDate = {};
      if (startDate) query.jobDate.$gte = new Date(startDate);
      if (endDate) query.jobDate.$lte = new Date(endDate);
    }

    const jobs = await Job.find(query)
      .populate('customer', 'name phone')
      .populate('vehicle', 'make model licensePlate')
      .populate('assignedMechanic', 'username role')
      .populate('services', 'name price')
      .populate('parts.item', 'name price')
      .limit(limit * 1)
      .skip((page - 1) * limit)
      .sort({ jobDate: -1 }); // Sort by most recent jobDate

    const count = await Job.countDocuments(query);

    res.status(200).json({
      jobs,
      totalPages: Math.ceil(count / limit),
      currentPage: parseInt(page),
      totalJobs: count,
    });
  } catch (error) {
    console.error('Error fetching jobs:', error);
    res.status(500).json({ message: 'Server error while fetching jobs.' });
  }
};

// Get a single job by ID
exports.getJobById = async (req, res) => {
  try {
    const { id } = req.params;
    if (!isValidObjectId(id)) {
        return res.status(400).json({ message: 'Invalid Job ID format.' });
    }

    const job = await Job.findOne({ _id: id, isDeleted: false })
      .populate('customer', 'name phone email address')
      .populate('vehicle', 'make model year vin licensePlate')
      .populate('assignedMechanic', 'username role')
      .populate('services', 'name price description category')
      .populate('parts.item', 'name price sku description category supplier');

    if (!job) {
      return res.status(404).json({ message: 'Job not found or has been deleted.' });
    }
    res.status(200).json(job);
  } catch (error) {
    console.error('Error fetching job by ID:', error);
    res.status(500).json({ message: 'Server error while fetching job.' });
  }
};

// Update an existing job
exports.updateJob = async (req, res) => {
  try {
    const { id } = req.params;
    if (!isValidObjectId(id)) {
        return res.status(400).json({ message: 'Invalid Job ID format.' });
    }

    const jobToUpdate = await Job.findOne({ _id: id, isDeleted: false });
    if (!jobToUpdate) {
      return res.status(404).json({ message: 'Job not found or has been deleted.' });
    }

    // Prevent updates if job is invoiced or cancelled, unless it's a status update by an admin perhaps
    if (['Invoiced', 'Cancelled', 'Deleted'].includes(jobToUpdate.status) && req.userRole !== 'admin') {
        // Allow admin to change status even if invoiced/cancelled, but not other fields easily.
        if (!req.body.status) { // If not trying to change status, block other changes for non-admins
             return res.status(403).json({ message: `Job is ${jobToUpdate.status} and cannot be updated by your role.` });
        }
    }


    const { description, status, services, parts, assignedMechanic, jobDate, estimatedCost, customerId, vehicleId } = req.body;

    // Validate and update fields
    if (description) jobToUpdate.description = description;
    if (status) jobToUpdate.status = status; // Consider validating status transitions
    if (jobDate) jobToUpdate.jobDate = jobDate;
    if (estimatedCost !== undefined) jobToUpdate.estimatedCost = estimatedCost;


    if (customerId && isValidObjectId(customerId)) {
        const customer = await Customer.findById(customerId);
        if (!customer || customer.isDeleted) return res.status(404).json({ message: 'New customer not found.' });
        jobToUpdate.customer = customerId;
    } else if (customerId) {
        return res.status(400).json({ message: 'Invalid new customer ID format.' });
    }

    if (vehicleId && isValidObjectId(vehicleId)) {
        const vehicle = await Vehicle.findById(vehicleId);
        if (!vehicle || vehicle.isDeleted) return res.status(404).json({ message: 'New vehicle not found.' });
        // Optional: Check if new vehicle belongs to current or new customer
        const currentCustomerId = jobToUpdate.customer.toString();
        if (vehicle.customer.toString() !== (customerId || currentCustomerId) ) {
            return res.status(400).json({ message: 'New vehicle does not belong to the job\'s customer.' });
        }
        jobToUpdate.vehicle = vehicleId;
    } else if (vehicleId) {
        return res.status(400).json({ message: 'Invalid new vehicle ID format.' });
    }


    if (assignedMechanic) {
        if (!isValidObjectId(assignedMechanic)) return res.status(400).json({ message: 'Invalid assigned mechanic ID.' });
        // You might want to check if the user exists and has the 'mechanic' role
        jobToUpdate.assignedMechanic = assignedMechanic;
    } else if (req.body.hasOwnProperty('assignedMechanic') && assignedMechanic === null) {
        jobToUpdate.assignedMechanic = null; // Allow unassigning
    }


    if (services) { // Handle full replacement of services
      jobToUpdate.services = [];
      for (const serviceId of services) {
        if (!isValidObjectId(serviceId)) return res.status(400).json({ message: `Invalid service ID: ${serviceId}`});
        const serviceExists = await Service.findById(serviceId);
        if (!serviceExists || serviceExists.isDeleted) return res.status(404).json({ message: `Service not found: ${serviceId}`});
        jobToUpdate.services.push(serviceId);
      }
    }

    if (parts) { // Handle full replacement of parts
      jobToUpdate.parts = [];
      for (const part of parts) {
        if (!part.item || !isValidObjectId(part.item) || !part.quantity || part.quantity < 1) {
          return res.status(400).json({ message: 'Invalid part data for update.' });
        }
        const inventoryItemExists = await InventoryItem.findById(part.item);
        if (!inventoryItemExists || inventoryItemExists.isDeleted) {
          return res.status(404).json({ message: `Inventory item not found: ${part.item}` });
        }
        jobToUpdate.parts.push({ item: part.item, quantity: part.quantity });
      }
    }

    await jobToUpdate.save();

    const populatedJob = await Job.findById(jobToUpdate._id)
        .populate('customer', 'name phone email')
        .populate('vehicle', 'make model licensePlate vin')
        .populate('assignedMechanic', 'username role')
        .populate('services', 'name price')
        .populate('parts.item', 'name price sku');

    res.status(200).json({ message: 'Job updated successfully', job: populatedJob });
  } catch (error) {
    console.error('Error updating job:', error);
    if (error.name === 'ValidationError') {
      return res.status(400).json({ message: 'Validation error', errors: error.errors });
    }
    res.status(500).json({ message: 'Server error while updating job.' });
  }
};

// Delete a job (soft delete)
exports.deleteJob = async (req, res) => {
  try {
    const { id } = req.params;
     if (!isValidObjectId(id)) {
        return res.status(400).json({ message: 'Invalid Job ID format.' });
    }

    const job = await Job.findById(id);
    if (!job || job.isDeleted) { // Check if already soft-deleted
      return res.status(404).json({ message: 'Job not found or already deleted.' });
    }

    // Instead of permanent deletion, mark as deleted
    job.isDeleted = true;
    job.status = 'Deleted'; // Or 'Cancelled' if that's more appropriate
    await job.save();

    res.status(200).json({ message: 'Job deleted successfully (soft delete).' });
  } catch (error) {
    console.error('Error deleting job:', error);
    res.status(500).json({ message: 'Server error while deleting job.' });
  }
};
