const mongoose = require('mongoose');

const serviceSchema = new mongoose.Schema({
  name: { type: String, required: true, trim: true },
  description: { type: String, trim: true },
  category: { type: String, trim: true },
  price: { type: Number, required: true, min: 0 },
  isDeleted: { type: Boolean, default: false, required: true },
}, { timestamps: true });

module.exports = mongoose.model('Service', serviceSchema);
