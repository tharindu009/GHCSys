const mongoose = require('mongoose');

const partUsageSchema = new mongoose.Schema({
  item: { type: mongoose.Schema.Types.ObjectId, ref: 'InventoryItem', required: true },
  quantity: { type: Number, required: true, min: 1 },
}, { _id: false });

const jobSchema = new mongoose.Schema({
  jobDate: { type: Date, default: Date.now, required: true },
  customer: { type: mongoose.Schema.Types.ObjectId, ref: 'Customer', required: true },
  vehicle: { type: mongoose.Schema.Types.ObjectId, ref: 'Vehicle', required: true },
  description: { type: String, required: true, trim: true },
  services: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Service' }],
  parts: [partUsageSchema],
  assignedMechanic: { type: mongoose.Schema.Types.ObjectId, ref: 'User' }, // Not always required initially
  status: {
    type: String,
    enum: ['Pending', 'Scheduled', 'In Progress', 'Awaiting Parts', 'Completed', 'Invoiced', 'Cancelled', 'Deleted'],
    default: 'Pending',
    required: true,
  },
  estimatedCost: { type: Number, min: 0 }, // Optional
  actualCost: { type: Number, min: 0 },    // Optional, can be calculated before invoicing
  isDeleted: { type: Boolean, default: false, required: true }, // For soft delete
}, { timestamps: true }); // Adds createdAt and updatedAt

module.exports = mongoose.model('Job', jobSchema);
