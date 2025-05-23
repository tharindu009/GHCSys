require('dotenv').config();
const express = require('express');
const mongoose = require('mongoose');
const cors = require('cors');

// Route imports
const authRoutes = require('./routes/auth.routes');
const jobRoutes = require('./routes/job.routes');
const invoiceRoutes = require('./routes/invoice.routes');
const inventoryRoutes = require('./routes/inventory.routes'); // Added inventory routes

// Middleware imports
const { verifyToken, isAdmin } = require('./middleware/authJwt');

const app = express();
const PORT = process.env.PORT || 3001; // Updated port from .env
const MONGODB_URI = process.env.MONGODB_URI || 'mongodb://localhost:27017/workshop_management_system'; // Updated URI from .env

// Middleware
app.use(cors());
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// Database connection
mongoose.connect(MONGODB_URI, { useNewUrlParser: true, useUnifiedTopology: true })
  .then(() => console.log('Connected to MongoDB'))
  .catch(err => {
    console.error('MongoDB connection error:', err);
    process.exit(1); // Exit process with failure
  });

// API Routes
app.use('/api/auth', authRoutes);
app.use('/api/jobs', jobRoutes);
app.use('/api/invoices', invoiceRoutes);
app.use('/api/inventory', inventoryRoutes); // Added inventory routes


// Health check route
app.get('/api/health', (req, res) => {
  res.status(200).json({ message: 'Server is running successfully', timestamp: new Date() });
});

// Protected test routes (can be removed or kept for diagnostics)
app.get('/api/test/user', verifyToken, (req, res) => {
  res.status(200).json({ message: 'Protected user route accessed successfully.', user: { id: req.userId, role: req.userRole } });
});

app.get('/api/test/admin', [verifyToken, isAdmin], (req, res) => {
  res.status(200).json({ message: 'Protected admin route accessed successfully.', user: { id: req.userId, role: req.userRole } });
});


// Global error handler (basic example)
app.use((err, req, res, next) => {
  console.error(err.stack);
  res.status(500).send('Something broke!');
});


// Start server
if (process.env.NODE_ENV !== 'test') { // Prevents server from starting during tests if not needed
  app.listen(PORT, () => {
    console.log(`Server listening on port ${PORT}`);
  });
}

module.exports = app; // Export for potential testing
