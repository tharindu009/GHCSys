const mongoose = require('mongoose');

const customerSchema = new mongoose.Schema({
  name: { type: String, required: true, trim: true },
  phone: { type: String, trim: true },
  email: { type: String, trim: true, lowercase: true }, // Store emails in lowercase
  address: { type: String, trim: true },
  isDeleted: { type: Boolean, default: false, required: true },
}, { timestamps: true });

module.exports = mongoose.model('Customer', customerSchema);
