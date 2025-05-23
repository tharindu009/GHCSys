const express = require('express');
const router = express.Router();
const inventoryController = require('../controllers/inventory.controller');
const { verifyToken, isAdmin, isServiceAdvisor, isMechanic } = require('../middleware/authJwt');

// Middleware for roles that can manage inventory (Admin, Service Advisor)
const canManageInventory = [
  verifyToken,
  (req, res, next) => {
    if (req.userRole === 'admin' || req.userRole === 'service_advisor') {
      next();
    } else {
      res.status(403).json({ message: 'Require Admin or Service Advisor Role!' });
    }
  }
];

// Middleware for roles that can view inventory (Admin, Service Advisor, Mechanic)
const canViewInventory = [
  verifyToken,
  (req, res, next) => {
    if (req.userRole === 'admin' || req.userRole === 'service_advisor' || req.userRole === 'mechanic') {
      next();
    } else {
      res.status(403).json({ message: 'Require Admin, Service Advisor, or Mechanic Role!' });
    }
  }
];

// Add a new inventory item
router.post(
  '/',
  canManageInventory,
  inventoryController.addInventoryItem
);

// Get all inventory items
router.get(
  '/',
  canViewInventory,
  inventoryController.getAllInventoryItems
);

// Get a single inventory item by ID
router.get(
  '/:id',
  canViewInventory,
  inventoryController.getInventoryItemById
);

// Get a single inventory item by SKU
router.get(
  '/sku/:sku',
  canViewInventory,
  inventoryController.getInventoryItemBySku
);

// Update an existing inventory item
router.put(
  '/:id',
  canManageInventory,
  inventoryController.updateInventoryItem
);

// Delete an inventory item (soft delete)
router.delete(
  '/:id',
  [verifyToken, isAdmin], // Only Admin can delete
  inventoryController.deleteInventoryItem
);

module.exports = router;
