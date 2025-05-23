import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import inventoryService from '../../services/inventoryService';
import '../../assets/css/InventoryForm.css'; // Create this for styling

const InventoryItemForm = ({ itemData, isEditMode = false }) => {
  const [formData, setFormData] = useState({
    name: '',
    sku: '',
    description: '',
    category: '',
    supplier: '',
    quantity: 0,
    price: 0,
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [formError, setFormError] = useState({}); // For field-specific errors
  const navigate = useNavigate();

  useEffect(() => {
    if (isEditMode && itemData) {
      setFormData({
        name: itemData.name || '',
        sku: itemData.sku || '',
        description: itemData.description || '',
        category: itemData.category || '',
        supplier: itemData.supplier || '',
        quantity: itemData.quantity || 0,
        price: itemData.price || 0,
      });
    }
  }, [isEditMode, itemData]);

  const handleChange = (e) => {
    const { name, value, type } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'number' ? parseFloat(value) : value,
    }));
    setFormError(prev => ({...prev, [name]: ''})); // Clear error on change
  };

  const validateForm = () => {
    const errors = {};
    if (!formData.name.trim()) errors.name = "Name is required.";
    if (!formData.sku.trim()) errors.sku = "SKU is required.";
    if (formData.quantity === undefined || formData.quantity < 0) errors.quantity = "Quantity must be a non-negative number.";
    if (formData.price === undefined || formData.price < 0) errors.price = "Price must be a non-negative number.";
    // Category and Supplier can be optional or add validation if required
    setFormError(errors);
    return Object.keys(errors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm()) {
        setError("Please fill all required fields correctly and ensure quantity/price are not negative.");
        return;
    }
    setLoading(true);
    setError('');
    try {
      if (isEditMode) {
        await inventoryService.updateInventoryItem(itemData._id, formData);
        navigate('/inventory'); // Or to item view page if one exists
      } else {
        await inventoryService.addInventoryItem(formData);
        navigate('/inventory');
      }
    } catch (err) {
      const message = err.message || `Failed to ${isEditMode ? 'update' : 'add'} item.`;
      setError(message);
      if (err.errors) { // Handle validation errors from backend (if structured this way)
        setFormError(err.errors);
      } else if (message.includes("SKU")) { // Specific check for SKU uniqueness error from backend
        setFormError(prev => ({...prev, sku: message}));
      }
      console.error("Form submission error:", err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="inventory-item-form">
      {error && <p className="error-message main-error">{error}</p>}
      
      <div className="form-group">
        <label htmlFor="name">Item Name:</label>
        <input type="text" id="name" name="name" value={formData.name} onChange={handleChange} required />
        {formError.name && <span className="form-error">{formError.name}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="sku">SKU (Stock Keeping Unit):</label>
        <input type="text" id="sku" name="sku" value={formData.sku} onChange={handleChange} required />
        {formError.sku && <span className="form-error">{formError.sku}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="description">Description:</label>
        <textarea id="description" name="description" value={formData.description} onChange={handleChange} />
      </div>
      
      <div className="form-group">
        <label htmlFor="category">Category:</label>
        <input type="text" id="category" name="category" value={formData.category} onChange={handleChange} />
      </div>

      <div className="form-group">
        <label htmlFor="supplier">Supplier:</label>
        <input type="text" id="supplier" name="supplier" value={formData.supplier} onChange={handleChange} />
      </div>

      <div className="form-group">
        <label htmlFor="quantity">Quantity:</label>
        <input type="number" id="quantity" name="quantity" value={formData.quantity} onChange={handleChange} required min="0" />
        {formError.quantity && <span className="form-error">{formError.quantity}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="price">Price ($):</label>
        <input type="number" id="price" name="price" value={formData.price} onChange={handleChange} required min="0" step="0.01" />
        {formError.price && <span className="form-error">{formError.price}</span>}
      </div>

      <button type="submit" className="btn btn-primary" disabled={loading}>
        {loading ? 'Saving...' : (isEditMode ? 'Update Item' : 'Add Item')}
      </button>
    </form>
  );
};

export default InventoryItemForm;
