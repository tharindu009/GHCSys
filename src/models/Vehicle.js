const mongoose = require('mongoose');

const vehicleSchema = new mongoose.Schema({
  make: { type: String, required: true, trim: true },
  model: { type: String, required: true, trim: true },
  year: { type: Number },
  vin: { type: String, unique: true, trim: true, uppercase: true }, // Store VINs in uppercase
  licensePlate: { type: String, trim: true, uppercase: true }, // Store license plates in uppercase
  customer: { type: mongoose.Schema.Types.ObjectId, ref: 'Customer', required: true },
  isDeleted: { type: Boolean, default: false, required: true },
}, { timestamps: true });

module.exports = mongoose.model('Vehicle', vehicleSchema);
