const mongoose = require('mongoose');

const userSchema = new mongoose.Schema({
  username: { type: String, required: true, unique: true, trim: true },
  passwordHash: { type: String, required: true },
  role: { type: String, enum: ['admin', 'mechanic', 'service_advisor'], default: 'mechanic', required: true },
});

// Basic validation for username and passwordHash will be implicitly handled by 'required: true'.
// Mongoose also provides more complex validation if needed, but for now, this is sufficient.

module.exports = mongoose.model('User', userSchema);
