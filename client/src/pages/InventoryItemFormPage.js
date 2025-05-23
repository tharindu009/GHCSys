import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import InventoryItemForm from '../components/inventory/InventoryItemForm';
import inventoryService from '../services/inventoryService';

function InventoryItemFormPage() {
  const { id: itemId } = useParams(); // Get itemId from URL if present
  const [itemData, setItemData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const isEditMode = Boolean(itemId);
  const navigate = useNavigate();

  useEffect(() => {
    if (isEditMode) {
      setLoading(true);
      setError('');
      inventoryService.getInventoryItemById(itemId)
        .then(data => {
          setItemData(data);
        })
        .catch(err => {
          setError(`Failed to fetch inventory item details: ${err.message}`);
          console.error(err);
          // Optionally redirect if item not found for edit
          // if (err.status === 404) navigate('/inventory', { replace: true });
        })
        .finally(() => setLoading(false));
    }
  }, [itemId, isEditMode, navigate]);

  if (loading && isEditMode) return <p>Loading item data for editing...</p>;
  if (error) return <p className="error-message">Error: {error}</p>;
  // If in edit mode and item not found after loading, show message or redirect
  if (isEditMode && !itemData && !loading) return <p>Inventory item not found.</p>; 

  return (
    <div className="inventory-item-form-page">
      <h1>{isEditMode ? 'Edit Inventory Item' : 'Add New Inventory Item'}</h1>
      <InventoryItemForm itemData={itemData} isEditMode={isEditMode} />
    </div>
  );
}

export default InventoryItemFormPage;
