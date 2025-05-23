const InventoryItem = require('../models/InventoryItem');
const mongoose = require('mongoose');

// Helper function to validate ObjectId
const isValidObjectId = (id) => mongoose.Types.ObjectId.isValid(id);

// Add a new inventory item
exports.addInventoryItem = async (req, res) => {
  try {
    const { name, description, quantity, price, supplier, sku, category } = req.body;

    // Basic Validations
    if (!name || !sku || quantity === undefined || price === undefined) {
      return res.status(400).json({ message: 'Name, SKU, quantity, and price are required.' });
    }
    if (parseFloat(price) < 0 || parseInt(quantity) < 0) {
        return res.status(400).json({ message: 'Price and quantity cannot be negative.' });
    }

    // Check for SKU uniqueness
    const existingItemBySku = await InventoryItem.findOne({ sku, isDeleted: false });
    if (existingItemBySku) {
      return res.status(400).json({ message: `SKU '${sku}' already exists.` });
    }

    const newItem = new InventoryItem({
      name,
      description,
      quantity,
      price,
      supplier,
      sku,
      category,
    });

    await newItem.save();
    res.status(201).json({ message: 'Inventory item added successfully', item: newItem });
  } catch (error) {
    console.error('Error adding inventory item:', error);
    if (error.name === 'ValidationError') {
      return res.status(400).json({ message: 'Validation error', errors: error.errors });
    }
    if (error.code === 11000) { // Duplicate key error for SKU from DB level
        return res.status(400).json({ message: `SKU '${req.body.sku}' already exists (database validation).` });
    }
    res.status(500).json({ message: 'Server error while adding inventory item.' });
  }
};

// Get all inventory items with filtering, pagination, and sorting
exports.getAllInventoryItems = async (req, res) => {
  try {
    const {
      category,
      supplier,
      name, // partial match
      sku,  // exact match
      quantity_lt, // quantity less than
      quantity_gt, // quantity greater than
      page = 1,
      limit = 10,
      sortBy = 'createdAt', // Default sort field
      sortOrder = 'desc',   // Default sort order (descending)
    } = req.query;

    const query = { isDeleted: false };

    if (category) query.category = { $regex: new RegExp(category, 'i') }; // Case-insensitive category match
    if (supplier) query.supplier = { $regex: new RegExp(supplier, 'i') }; // Case-insensitive supplier match
    if (name) query.name = { $regex: new RegExp(name, 'i') }; // Case-insensitive partial name match
    if (sku) query.sku = sku; // Exact SKU match

    if (quantity_lt !== undefined) {
      const qtyLt = parseInt(quantity_lt);
      if (!isNaN(qtyLt)) {
        query.quantity = { ...query.quantity, $lt: qtyLt };
      }
    }
    if (quantity_gt !== undefined) {
      const qtyGt = parseInt(quantity_gt);
      if (!isNaN(qtyGt)) {
        query.quantity = { ...query.quantity, $gt: qtyGt };
      }
    }
    
    const sortOptions = {};
    if (sortBy && ['name', 'sku', 'category', 'quantity', 'price', 'supplier', 'createdAt', 'updatedAt'].includes(sortBy)) {
        sortOptions[sortBy] = sortOrder === 'asc' ? 1 : -1;
    } else {
        sortOptions['createdAt'] = -1; // Default sort
    }


    const items = await InventoryItem.find(query)
      .sort(sortOptions)
      .limit(limit * 1)
      .skip((page - 1) * limit);

    const count = await InventoryItem.countDocuments(query);

    res.status(200).json({
      items,
      totalPages: Math.ceil(count / limit),
      currentPage: parseInt(page),
      totalItems: count,
    });
  } catch (error) {
    console.error('Error fetching inventory items:', error);
    res.status(500).json({ message: 'Server error while fetching inventory items.' });
  }
};

// Get a single inventory item by ID
exports.getInventoryItemById = async (req, res) => {
  try {
    const { id } = req.params;
    if (!isValidObjectId(id)) {
      return res.status(400).json({ message: 'Invalid Item ID format.' });
    }

    const item = await InventoryItem.findOne({ _id: id, isDeleted: false });

    if (!item) {
      return res.status(404).json({ message: 'Inventory item not found or has been deleted.' });
    }
    res.status(200).json(item);
  } catch (error) {
    console.error('Error fetching inventory item by ID:', error);
    res.status(500).json({ message: 'Server error while fetching inventory item.' });
  }
};

// Get a single inventory item by SKU
exports.getInventoryItemBySku = async (req, res) => {
  try {
    const { sku } = req.params;
    if (!sku || sku.trim() === '') {
        return res.status(400).json({ message: 'SKU must be provided.' });
    }

    const item = await InventoryItem.findOne({ sku: sku, isDeleted: false });

    if (!item) {
      return res.status(404).json({ message: `Inventory item with SKU '${sku}' not found or has been deleted.` });
    }
    res.status(200).json(item);
  } catch (error) {
    console.error('Error fetching inventory item by SKU:', error);
    res.status(500).json({ message: 'Server error while fetching inventory item.' });
  }
};


// Update an existing inventory item
exports.updateInventoryItem = async (req, res) => {
  try {
    const { id } = req.params;
    if (!isValidObjectId(id)) {
      return res.status(400).json({ message: 'Invalid Item ID format.' });
    }

    const itemToUpdate = await InventoryItem.findById(id);
    if (!itemToUpdate) {
      return res.status(404).json({ message: 'Inventory item not found.' });
    }

    // If item is soft-deleted and not trying to un-delete, prevent update
    if (itemToUpdate.isDeleted && req.body.isDeleted !== false) {
        return res.status(403).json({ message: 'Cannot update a deleted item unless you are restoring it (set isDeleted: false).' });
    }

    const { name, description, quantity, price, supplier, sku, category, isDeleted } = req.body;

    // SKU Uniqueness Check if SKU is being changed
    if (sku && sku !== itemToUpdate.sku) {
      const existingItemBySku = await InventoryItem.findOne({ sku, _id: { $ne: id }, isDeleted: false });
      if (existingItemBySku) {
        return res.status(400).json({ message: `SKU '${sku}' already exists for another item.` });
      }
      itemToUpdate.sku = sku;
    }

    if (name) itemToUpdate.name = name;
    if (description !== undefined) itemToUpdate.description = description;
    if (quantity !== undefined) {
        if (parseInt(quantity) < 0) return res.status(400).json({ message: 'Quantity cannot be negative.' });
        itemToUpdate.quantity = quantity;
    }
    if (price !== undefined) {
        if (parseFloat(price) < 0) return res.status(400).json({ message: 'Price cannot be negative.' });
        itemToUpdate.price = price;
    }
    if (supplier !== undefined) itemToUpdate.supplier = supplier;
    if (category !== undefined) itemToUpdate.category = category;
    
    // Handle soft delete / undelete
    if (isDeleted !== undefined && typeof isDeleted === 'boolean') {
        itemToUpdate.isDeleted = isDeleted;
    }


    await itemToUpdate.save();
    res.status(200).json({ message: 'Inventory item updated successfully', item: itemToUpdate });
  } catch (error) {
    console.error('Error updating inventory item:', error);
    if (error.name === 'ValidationError') {
      return res.status(400).json({ message: 'Validation error', errors: error.errors });
    }
    if (error.code === 11000) { // Duplicate key error for SKU
        return res.status(400).json({ message: `SKU '${req.body.sku}' already exists (database validation).` });
    }
    res.status(500).json({ message: 'Server error while updating inventory item.' });
  }
};

// Delete an inventory item (soft delete)
exports.deleteInventoryItem = async (req, res) => {
  try {
    const { id } = req.params;
    if (!isValidObjectId(id)) {
      return res.status(400).json({ message: 'Invalid Item ID format.' });
    }

    const item = await InventoryItem.findById(id);
    if (!item || item.isDeleted) { // Check if already soft-deleted
      return res.status(404).json({ message: 'Inventory item not found or already deleted.' });
    }

    item.isDeleted = true;
    await item.save();

    res.status(200).json({ message: 'Inventory item deleted successfully (soft delete).' });
  } catch (error) {
    console.error('Error deleting inventory item:', error);
    res.status(500).json({ message: 'Server error while deleting inventory item.' });
  }
};
