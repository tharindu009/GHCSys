import React, { useState, useEffect, useCallback } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import invoiceService from '../services/invoiceService';
import jobService from '../services/jobService'; // Potentially to update job status if invoice is voided
import { useAuth } from '../contexts/AuthContext';
import '../assets/css/InvoiceView.css'; // Create this for styling

function InvoiceViewPage() {
  const { id: invoiceId } = useParams();
  const [invoice, setInvoice] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [newStatus, setNewStatus] = useState('');
  const { currentUser } = useAuth();
  const navigate = useNavigate();

  const fetchInvoiceDetails = useCallback(async () => {
    setLoading(true);
    setError('');
    try {
      const data = await invoiceService.getInvoiceById(invoiceId);
      setInvoice(data);
      setNewStatus(data.paymentStatus || ''); // Initialize dropdown with current status
    } catch (err) {
      setError(err.message || 'Failed to fetch invoice details.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  }, [invoiceId]);

  useEffect(() => {
    fetchInvoiceDetails();
  }, [fetchInvoiceDetails]);

  const handleStatusUpdate = async (e) => {
    e.preventDefault();
    if (!newStatus) {
      setError('Please select a new status.');
      return;
    }
    if (newStatus === invoice.paymentStatus) {
      setError('The new status is the same as the current status.');
      return;
    }
    setLoading(true);
    setError('');
    try {
      const updatedInvoice = await invoiceService.updateInvoiceStatus(invoiceId, newStatus);
      setInvoice(updatedInvoice.invoice); // Backend returns { message, invoice }
      setNewStatus(updatedInvoice.invoice.paymentStatus);
      alert('Invoice status updated successfully!');
      // If status changed to Void or Deleted, and job was 'Invoiced', maybe revert job status
      if ((newStatus === 'Void' || newStatus === 'Deleted') && invoice.job?.status === 'Invoiced') {
        try {
            await jobService.updateJob(invoice.job._id, { status: 'Completed' });
            console.log(`Job ${invoice.job._id} status reverted to Completed.`);
        } catch (jobErr) {
            console.error("Error reverting job status:", jobErr);
            // Non-critical, so don't necessarily show error to user for this part
        }
      }

    } catch (err) {
      setError(err.message || 'Failed to update invoice status.');
    } finally {
      setLoading(false);
    }
  };
  
  const handleDeleteInvoice = async () => {
    if (window.confirm('Are you sure you want to delete (void) this invoice? This action is a soft delete.')) {
      setLoading(true);
      setError('');
      try {
        await invoiceService.deleteInvoice(invoiceId);
        alert('Invoice deleted successfully.');
        // Optionally, revert job status if applicable
        if (invoice.job?.status === 'Invoiced') {
             await jobService.updateJob(invoice.job._id, { status: 'Completed' });
        }
        navigate('/invoices');
      } catch (err) {
        setError(err.message || 'Failed to delete invoice.');
        setLoading(false);
      }
    }
  };


  if (loading && !invoice) return <p className="loading-message">Loading invoice details...</p>;
  if (error) return <p className="error-message">Error: {error}</p>;
  if (!invoice) return <p>Invoice not found.</p>;

  const canManageInvoice = currentUser && (currentUser.role === 'admin' || currentUser.role === 'service_advisor');
  const canDeleteInvoice = currentUser && currentUser.role === 'admin'; // Only admin can delete

  return (
    <div className="invoice-view-page">
      <h1>Invoice Details: {invoice.invoiceNumber}</h1>

      <div className="invoice-actions-bar">
        {canManageInvoice && (
          <form onSubmit={handleStatusUpdate} className="status-update-form">
            <select value={newStatus} onChange={(e) => setNewStatus(e.target.value)} disabled={loading}>
              <option value="Pending">Pending</option>
              <option value="Paid">Paid</option>
              <option value="Partially Paid">Partially Paid</option>
              <option value="Overdue">Overdue</option>
              <option value="Void">Void</option>
              {/* 'Deleted' status might be set by delete action only */}
            </select>
            <button type="submit" className="btn btn-primary" disabled={loading || newStatus === invoice.paymentStatus}>
              {loading ? 'Updating...' : 'Update Status'}
            </button>
          </form>
        )}
        {canDeleteInvoice && (
             <button onClick={handleDeleteInvoice} className="btn btn-danger" disabled={loading}>
                Delete Invoice
            </button>
        )}
        <Link to="/invoices" className="btn btn-secondary">Back to Invoices</Link>
      </div>

      <div className="invoice-details-grid">
        <div className="detail-item"><strong>Invoice Number:</strong> {invoice.invoiceNumber}</div>
        <div className="detail-item"><strong>Status:</strong> <span className={`status-badge status-invoice-${invoice.paymentStatus?.toLowerCase().replace(/\s+/g, '-')}`}>{invoice.paymentStatus}</span></div>
        <div className="detail-item"><strong>Invoice Date:</strong> {new Date(invoice.invoiceDate).toLocaleDateString()}</div>
        <div className="detail-item">
            <strong>Job ID:</strong> 
            {invoice.job?._id ? <Link to={`/jobs/${invoice.job._id}`}>{invoice.job._id}</Link> : 'N/A'}
        </div>
      </div>

      <h2>Customer & Vehicle Information</h2>
      {invoice.job ? (
        <>
          <div className="invoice-details-grid">
            <div className="detail-item"><strong>Customer:</strong> {invoice.job.customer?.name || 'N/A'}</div>
            <div className="detail-item"><strong>Phone:</strong> {invoice.job.customer?.phone || 'N/A'}</div>
            <div className="detail-item"><strong>Email:</strong> {invoice.job.customer?.email || 'N/A'}</div>
          </div>
          <div className="invoice-details-grid" style={{marginTop: '10px'}}>
            <div className="detail-item"><strong>Vehicle:</strong> {invoice.job.vehicle?.make} {invoice.job.vehicle?.model}</div>
            <div className="detail-item"><strong>VIN:</strong> {invoice.job.vehicle?.vin || 'N/A'}</div>
            <div className="detail-item"><strong>License:</strong> {invoice.job.vehicle?.licensePlate || 'N/A'}</div>
          </div>
        </>
      ) : <p>Associated job details not available.</p>}
      

      <h2>Line Items</h2>
      {invoice.lineItems && invoice.lineItems.length > 0 ? (
        <table className="line-items-table">
          <thead>
            <tr>
              <th>Description</th>
              <th>Quantity</th>
              <th>Unit Price</th>
              <th>Total Price</th>
            </tr>
          </thead>
          <tbody>
            {invoice.lineItems.map((item, index) => (
              <tr key={index}>
                <td>{item.description}</td>
                <td>{item.quantity}</td>
                <td>${item.unitPrice?.toFixed(2)}</td>
                <td>${item.totalPrice?.toFixed(2)}</td>
              </tr>
            ))}
          </tbody>
          <tfoot>
            <tr>
              <td colSpan="3" style={{ textAlign: 'right', fontWeight: 'bold' }}>Total Amount:</td>
              <td style={{ fontWeight: 'bold' }}>${invoice.totalAmount?.toFixed(2)}</td>
            </tr>
          </tfoot>
        </table>
      ) : <p>No line items for this invoice.</p>}

      <div className="timestamps">
        <p><strong>Created At:</strong> {new Date(invoice.createdAt).toLocaleString()}</p>
        <p><strong>Last Updated:</strong> {new Date(invoice.updatedAt).toLocaleString()}</p>
      </div>
    </div>
  );
}

export default InvoiceViewPage;
