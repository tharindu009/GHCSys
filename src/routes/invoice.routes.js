const express = require('express');
const router = express.Router();
const invoiceController = require('../controllers/invoice.controller');
const { verifyToken, isAdmin, isServiceAdvisor } = require('../middleware/authJwt');

const canManageInvoices = [
  verifyToken,
  (req, res, next) => {
    if (req.userRole === 'admin' || req.userRole === 'service_advisor') {
      next();
    } else {
      res.status(403).json({ message: 'Require Admin or Service Advisor Role!' });
    }
  }
];

// Create a new invoice from a job
router.post(
  '/',
  canManageInvoices,
  invoiceController.createInvoiceFromJob
);

// Get all invoices
router.get(
  '/',
  canManageInvoices,
  invoiceController.getAllInvoices
);

// Get a single invoice by ID
router.get(
  '/:id',
  canManageInvoices,
  invoiceController.getInvoiceById
);

// Update an invoice's payment status
router.put(
  '/:id/status',
  canManageInvoices,
  invoiceController.updateInvoiceStatus
);

// Delete an invoice (soft delete, e.g., set status to Void or Deleted)
router.delete(
  '/:id',
  [verifyToken, isAdmin], // Typically only Admin should hard delete or void in a way that's final
  invoiceController.deleteInvoice
);

module.exports = router;
