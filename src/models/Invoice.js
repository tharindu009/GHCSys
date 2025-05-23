const mongoose = require('mongoose');
const AutoIncrement = require('mongoose-sequence')(mongoose);

const lineItemSchema = new mongoose.Schema({
  description: { type: String, required: true },
  quantity: { type: Number, required: true, min: 1 },
  unitPrice: { type: Number, required: true, min: 0 },
  totalPrice: { type: Number, required: true, min: 0 },
}, { _id: false });

const invoiceSchema = new mongoose.Schema({
  job: { type: mongoose.Schema.Types.ObjectId, ref: 'Job', required: true },
  invoiceNumber: { type: String, unique: true }, // Will be auto-generated
  invoiceDate: { type: Date, default: Date.now, required: true },
  lineItems: [lineItemSchema],
  totalAmount: { type: Number, required: true, min: 0 },
  paymentStatus: {
    type: String,
    enum: ['Pending', 'Paid', 'Partially Paid', 'Overdue', 'Void', 'Deleted'],
    default: 'Pending',
    required: true,
  },
  isDeleted: { type: Boolean, default: false, required: true }, // For soft delete
}, { timestamps: true }); // Adds createdAt and updatedAt

// Auto-generate invoiceNumber before saving
// Using a more robust approach than mongoose-sequence for custom string formatting if needed,
// but for simple increment, mongoose-sequence is fine.
// For now, let's assume a simpler approach or a plugin like mongoose-sequence would be added.
// We will handle sequence generation in the controller for more control if mongoose-sequence is not used.

// If using mongoose-sequence, you would uncomment the following:
// invoiceSchema.plugin(AutoIncrement, { inc_field: 'invoice_sequence_num', id: 'invoice_counter' });
// And then use a pre-save hook to format invoiceNumber e.g. INV-0001

module.exports = mongoose.model('Invoice', invoiceSchema);
