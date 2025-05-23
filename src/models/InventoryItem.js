const mongoose = require('mongoose');

const inventoryItemSchema = new mongoose.Schema({
  name: { type: String, required: true, trim: true },
  description: { type: String, trim: true },
  sku: { type: String, unique: true, trim: true }, // Stock Keeping Unit
  category: { type: String, trim: true },
  quantity: { type: Number, required: true, default: 0, min: 0 },
  price: { type: Number, required: true, min: 0 },
  supplier: { type: String, trim: true },
  isDeleted: { type: Boolean, default: false, required: true },
}, { timestamps: true });

module.exports = mongoose.model('InventoryItem', inventoryItemSchema);
