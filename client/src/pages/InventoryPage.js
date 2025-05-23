import React, { useState, useEffect, useCallback } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import inventoryService from '../services/inventoryService';
import { useAuth } from '../contexts/AuthContext';
import '../assets/css/Inventory.css'; // Create this file later for styling

function InventoryPage() {
  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [filters, setFilters] = useState({
    name: '',
    sku: '',
    category: '',
    supplier: '',
    quantity_lt: '', // Quantity less than
  });
  const navigate = useNavigate();
  const { currentUser } = useAuth();

  const fetchInventoryItems = useCallback(async () => {
    setLoading(true);
    setError('');
    try {
      const params = {
        page: currentPage,
        limit: 10, 
        name: filters.name || undefined,
        sku: filters.sku || undefined,
        category: filters.category || undefined,
        supplier: filters.supplier || undefined,
        quantity_lt: filters.quantity_lt || undefined,
      };
      const data = await inventoryService.getAllInventoryItems(params);
      setItems(data.items || []);
      setTotalPages(data.totalPages || 1);
    } catch (err) {
      setError(err.message || 'Failed to fetch inventory items.');
    } finally {
      setLoading(false);
    }
  }, [currentPage, filters]);

  useEffect(() => {
    fetchInventoryItems();
  }, [fetchInventoryItems]);

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters(prev => ({ ...prev, [name]: value }));
    setCurrentPage(1); 
  };

  const handleDelete = async (itemId) => {
    if (window.confirm('Are you sure you want to delete this inventory item?')) {
      try {
        await inventoryService.deleteInventoryItem(itemId);
        fetchInventoryItems(); // Refetch to update list
      } catch (err) {
        setError(err.message || 'Failed to delete inventory item.');
      }
    }
  };

  const canManage = currentUser && (currentUser.role === 'admin' || currentUser.role === 'service_advisor');

  if (loading) return <div className="loading-message">Loading inventory...</div>;
  if (error) return <div className="error-message">Error: {error}</div>;

  return (
    <div className="inventory-page">
      <h1>Inventory Management</h1>
      {canManage && (
        <div className="actions-bar">
          <Link to="/inventory/new" className="btn btn-primary">Add New Item</Link>
        </div>
      )}

      <div className="filters-container inventory-filters">
        <input type="text" name="name" placeholder="Search by Name..." value={filters.name} onChange={handleFilterChange} className="filter-input"/>
        <input type="text" name="sku" placeholder="Search by SKU..." value={filters.sku} onChange={handleFilterChange} className="filter-input"/>
        <input type="text" name="category" placeholder="Filter by Category..." value={filters.category} onChange={handleFilterChange} className="filter-input"/>
        <input type="text" name="supplier" placeholder="Filter by Supplier..." value={filters.supplier} onChange={handleFilterChange} className="filter-input"/>
        <input type="number" name="quantity_lt" placeholder="Stock <" value={filters.quantity_lt} onChange={handleFilterChange} className="filter-input" style={{width: '120px'}}/>
      </div>

      <table className="inventory-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>SKU</th>
            <th>Category</th>
            <th>Supplier</th>
            <th>Quantity</th>
            <th>Price</th>
            {canManage && <th>Actions</th>}
          </tr>
        </thead>
        <tbody>
          {items.length > 0 ? items.map(item => (
            <tr key={item._id}>
              <td>{item.name}</td>
              <td>{item.sku}</td>
              <td>{item.category || 'N/A'}</td>
              <td>{item.supplier || 'N/A'}</td>
              <td>{item.quantity}</td>
              <td>${item.price?.toFixed(2)}</td>
              {canManage && (
                <td>
                  <button onClick={() => navigate(`/inventory/${item._id}/edit`)} className="btn btn-sm btn-warning">Edit</button>
                  <button onClick={() => handleDelete(item._id)} className="btn btn-sm btn-danger">Delete</button>
                </td>
              )}
            </tr>
          )) : (
            <tr>
              <td colSpan={canManage ? "7" : "6"}>No inventory items found matching your criteria.</td>
            </tr>
          )}
        </tbody>
      </table>

      <div className="pagination-controls">
        <button 
          onClick={() => setCurrentPage(prev => Math.max(prev - 1, 1))} 
          disabled={currentPage === 1 || totalPages === 0}
        >
          Previous
        </button>
        <span>Page {currentPage} of {totalPages || 1}</span>
        <button 
          onClick={() => setCurrentPage(prev => Math.min(prev + 1, totalPages))} 
          disabled={currentPage === totalPages || totalPages === 0}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default InventoryPage;
