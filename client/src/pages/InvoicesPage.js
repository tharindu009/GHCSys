import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import invoiceService from '../services/invoiceService';
import '../assets/css/Invoices.css'; // Create this file later for styling

function InvoicesPage() {
  const [invoices, setInvoices] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [filters, setFilters] = useState({ paymentStatus: '', customerSearch: '' });
  const navigate = useNavigate();

  const fetchInvoices = useCallback(async () => {
    setLoading(true);
    setError('');
    try {
      const params = {
        page: currentPage,
        limit: 10, 
        paymentStatus: filters.paymentStatus || undefined,
        // If backend directly supports searching invoices by customer name (e.g. via job.customer.name)
        // customerName: filters.customerSearch || undefined, 
      };
      const data = await invoiceService.getAllInvoices(params);
      setInvoices(data.invoices || []);
      setTotalPages(data.totalPages || 1);
    } catch (err) {
      setError(err.message || 'Failed to fetch invoices.');
    } finally {
      setLoading(false);
    }
  }, [currentPage, filters.paymentStatus]); // customerSearch removed from deps if client-side only

  useEffect(() => {
    fetchInvoices();
  }, [fetchInvoices]);

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters(prev => ({ ...prev, [name]: value }));
    setCurrentPage(1); 
  };

  // Client-side filtering for customer name. Requires job.customer.name to be populated.
  const displayedInvoices = filters.customerSearch
    ? invoices.filter(invoice =>
        invoice.job?.customer?.name?.toLowerCase().includes(filters.customerSearch.toLowerCase())
      )
    : invoices;

  // Deletion is typically handled on the view page or by admins with specific rights.
  // For this list view, direct deletion might not be common.
  // const handleDelete = async (invoiceId) => { ... }

  if (loading) return <div className="loading-message">Loading invoices...</div>;
  if (error) return <div className="error-message">Error: {error}</div>;

  return (
    <div className="invoices-page">
      <h1>Invoice Management</h1>
      
      <div className="filters-container">
        <input 
          type="text"
          name="customerSearch"
          placeholder="Search by Customer Name..."
          value={filters.customerSearch}
          onChange={handleFilterChange}
          className="filter-input"
        />
        <select 
          name="paymentStatus" 
          value={filters.paymentStatus} 
          onChange={handleFilterChange}
          className="filter-select"
        >
          <option value="">All Statuses</option>
          <option value="Pending">Pending</option>
          <option value="Paid">Paid</option>
          <option value="Partially Paid">Partially Paid</option>
          <option value="Overdue">Overdue</option>
          <option value="Void">Void</option>
          <option value="Deleted">Deleted</option>
        </select>
      </div>

      <table className="invoices-table">
        <thead>
          <tr>
            <th>Invoice #</th>
            <th>Job ID</th>
            <th>Customer Name</th>
            <th>Invoice Date</th>
            <th>Total Amount</th>
            <th>Payment Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {displayedInvoices.length > 0 ? displayedInvoices.map(invoice => (
            <tr key={invoice._id}>
              <td>{invoice.invoiceNumber}</td>
              <td>{invoice.job?._id ? invoice.job._id.substring(0, 8) + '...' : 'N/A'}</td>
              <td>{invoice.job?.customer?.name || 'N/A'}</td>
              <td>{new Date(invoice.invoiceDate).toLocaleDateString()}</td>
              <td>${invoice.totalAmount?.toFixed(2)}</td>
              <td><span className={`status-badge status-invoice-${invoice.paymentStatus?.toLowerCase().replace(/\s+/g, '-')}`}>{invoice.paymentStatus}</span></td>
              <td>
                <button onClick={() => navigate(`/invoices/${invoice._id}`)} className="btn btn-sm btn-info">View</button>
                {/* Delete button might be restricted to admins on the view page */}
              </td>
            </tr>
          )) : (
            <tr>
              <td colSpan="7">No invoices found matching your criteria.</td>
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

export default InvoicesPage;
