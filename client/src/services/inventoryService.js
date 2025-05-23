import apiClient from './api';

const addInventoryItem = async (itemData) => {
  try {
    const response = await apiClient.post('/inventory', itemData);
    return response.data;
  } catch (error) {
    console.error('Error adding inventory item:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to add inventory item');
  }
};

const getAllInventoryItems = async (params) => {
  try {
    const response = await apiClient.get('/inventory', { params });
    return response.data; // Expects { items: [], totalPages, currentPage, totalItems }
  } catch (error) {
    console.error('Error fetching inventory items:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch inventory items');
  }
};

const getInventoryItemById = async (itemId) => {
  try {
    const response = await apiClient.get(`/inventory/${itemId}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching inventory item ${itemId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch inventory item details');
  }
};

const getInventoryItemBySku = async (sku) => {
    try {
      const response = await apiClient.get(`/inventory/sku/${sku}`);
      return response.data;
    } catch (error) {
      console.error(`Error fetching inventory item by SKU ${sku}:`, error.response ? error.response.data : error.message);
      throw error.response ? error.response.data : new Error('Failed to fetch inventory item by SKU');
    }
  };

const updateInventoryItem = async (itemId, itemData) => {
  try {
    const response = await apiClient.put(`/inventory/${itemId}`, itemData);
    return response.data;
  } catch (error) {
    console.error(`Error updating inventory item ${itemId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to update inventory item');
  }
};

const deleteInventoryItem = async (itemId) => {
  try {
    const response = await apiClient.delete(`/inventory/${itemId}`);
    return response.data; // Expects { message: 'Inventory item deleted successfully (soft delete).' }
  } catch (error) {
    console.error(`Error deleting inventory item ${itemId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to delete inventory item');
  }
};

const inventoryService = {
  addInventoryItem,
  getAllInventoryItems,
  getInventoryItemById,
  getInventoryItemBySku,
  updateInventoryItem,
  deleteInventoryItem,
};

export default inventoryService;
