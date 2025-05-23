import React, { useState, useEffect, useCallback } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import jobService from '../services/jobService';
import invoiceService from '../services/invoiceService'; // Added invoiceService
import { useAuth } from '../contexts/AuthContext';
import '../assets/css/JobView.css';

function JobViewPage() {
  const { id: jobId } = useParams();
  const [job, setJob] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [invoiceError, setInvoiceError] = useState(''); // Specific error for invoice creation
  const { currentUser } = useAuth();
  const navigate = useNavigate();

  const fetchJobDetails = useCallback(async () => {
    setLoading(true);
    setError('');
    setInvoiceError('');
    try {
      const data = await jobService.getJobById(jobId);
      setJob(data);
    } catch (err) {
      setError(err.message || 'Failed to fetch job details.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  }, [jobId]);

  useEffect(() => {
    fetchJobDetails();
  }, [fetchJobDetails]);

  const handleDelete = async () => {
    if (window.confirm('Are you sure you want to delete this job?')) {
      try {
        await jobService.deleteJob(jobId);
        navigate('/jobs'); 
      } catch (err) {
        setError(err.message || 'Failed to delete job.');
      }
    }
  };

  const handleGenerateInvoice = async () => {
    setInvoiceError('');
    if (!jobId) {
        setInvoiceError('Job ID is missing.');
        return;
    }
    if (window.confirm('Are you sure you want to generate an invoice for this job?')) {
        try {
            setLoading(true); // Use main loading state or a specific one for this action
            const response = await invoiceService.createInvoiceFromJob(jobId);
            alert(response.message || 'Invoice created successfully!');
            // Option 1: Navigate to the new invoice
            if (response.invoice && response.invoice._id) {
                 navigate(`/invoices/${response.invoice._id}`);
            } else {
            // Option 2: Or refresh job details to show it's invoiced
                fetchJobDetails(); 
            }
        } catch (err) {
            setInvoiceError(err.message || 'Failed to generate invoice.');
            console.error("Invoice generation error:", err);
        } finally {
            setLoading(false);
        }
    }
  };


  if (loading && !job) return <p className="loading-message">Loading job details...</p>;
  if (error) return <p className="error-message">Error: {error}</p>;
  if (!job) return <p>Job not found.</p>;

  const canModify = currentUser && (currentUser.role === 'admin' || currentUser.role === 'service_advisor');
  const canGenerateInvoice = canModify && job.status === 'Completed';
  // More robust check for existing invoice: job.status !== 'Invoiced' (assuming backend sets this)
  // Or if backend adds an invoiceId to the job model: !job.invoiceId

  return (
    <div className="job-view-page">
      <h1>Job Details: {job.invoiceNumber || job._id}</h1> {/* Display invoiceNumber if available */}
      
      {invoiceError && <p className="error-message" style={{backgroundColor: '#ffdddd', color: '#d8000c'}}>{invoiceError}</p>}

      <div className="job-actions-bar">
        {canModify && (
          <>
            <Link to={`/jobs/${jobId}/edit`} className="btn btn-warning">Edit Job</Link>
            <button onClick={handleDelete} className="btn btn-danger">Delete Job</button>
          </>
        )}
        {canGenerateInvoice && job.status !== 'Invoiced' && ( // Check if not already invoiced
             <button onClick={handleGenerateInvoice} className="btn btn-success">Generate Invoice</button>
        )}
        <Link to="/jobs" className="btn btn-secondary">Back to Jobs List</Link>
      </div>

      <div className="job-details-grid">
        <div className="detail-item"><strong>Job ID:</strong> {job._id}</div>
        <div className="detail-item"><strong>Status:</strong> <span className={`status-badge status-${job.status?.toLowerCase().replace(/\s+/g, '-')}`}>{job.status}</span></div>
        <div className="detail-item"><strong>Job Date:</strong> {new Date(job.jobDate).toLocaleDateString()}</div>
        <div className="detail-item"><strong>Estimated Cost:</strong> ${job.estimatedCost?.toFixed(2) || 'N/A'}</div>
        <div className="detail-item"><strong>Actual Cost:</strong> ${job.actualCost?.toFixed(2) || 'N/A'}</div>
         <div className="detail-item full-width"><strong>Description:</strong> <p>{job.description}</p></div>
      </div>

      <h2>Customer & Vehicle</h2>
      <div className="job-details-grid">
        {job.customer ? (
            <>
                <div className="detail-item"><strong>Customer Name:</strong> {job.customer.name}</div>
                <div className="detail-item"><strong>Customer Phone:</strong> {job.customer.phone || 'N/A'}</div>
                <div className="detail-item"><strong>Customer Email:</strong> {job.customer.email || 'N/A'}</div>
            </>
        ) : <div className="detail-item">Customer details not available.</div>}
      </div>
      <div className="job-details-grid" style={{marginTop: '10px'}}>
        {job.vehicle ? (
            <>
                <div className="detail-item"><strong>Vehicle Make:</strong> {job.vehicle.make}</div>
                <div className="detail-item"><strong>Vehicle Model:</strong> {job.vehicle.model}</div>
                <div className="detail-item"><strong>VIN:</strong> {job.vehicle.vin || 'N/A'}</div>
                <div className="detail-item"><strong>License Plate:</strong> {job.vehicle.licensePlate || 'N/A'}</div>
            </>
        ) : <div className="detail-item">Vehicle details not available.</div>}
      </div>

      <h2>Assigned Mechanic</h2>
      <div className="job-details-grid">
        <div className="detail-item">
            <strong>Mechanic:</strong> {job.assignedMechanic ? job.assignedMechanic.username : 'Not assigned'}
        </div>
        {job.assignedMechanic && <div className="detail-item"><strong>Role:</strong> {job.assignedMechanic.role}</div>}
      </div>

      <h2>Services</h2>
      {job.services && job.services.length > 0 ? (
        <ul className="details-list">
          {job.services.map(service => (
            <li key={service._id || service} className="list-item">
              {typeof service === 'object' ? `${service.name} - $${service.price?.toFixed(2)}` : `Service ID: ${service}`}
              {typeof service === 'object' && service.description && <p className="service-description">{service.description}</p>}
            </li>
          ))}
        </ul>
      ) : <p>No services listed for this job.</p>}

      <h2>Parts Used</h2>
      {job.parts && job.parts.length > 0 ? (
        <table className="parts-table">
            <thead>
                <tr>
                    <th>Part Name</th>
                    <th>SKU</th>
                    <th>Quantity</th>
                    <th>Unit Price</th>
                    <th>Total Price</th>
                </tr>
            </thead>
            <tbody>
            {job.parts.map((partUsage, index) => (
                <tr key={partUsage.item?._id || index}>
                <td>{partUsage.item ? partUsage.item.name : 'N/A'}</td>
                <td>{partUsage.item ? partUsage.item.sku : 'N/A'}</td>
                <td>{partUsage.quantity}</td>
                <td>${partUsage.item ? partUsage.item.price?.toFixed(2) : 'N/A'}</td>
                <td>${partUsage.item ? (partUsage.quantity * partUsage.item.price).toFixed(2) : 'N/A'}</td>
                </tr>
            ))}
            </tbody>
        </table>
      ) : <p>No parts listed for this job.</p>}
      
      <div className="timestamps">
        <p><strong>Created At:</strong> {new Date(job.createdAt).toLocaleString()}</p>
        <p><strong>Last Updated:</strong> {new Date(job.updatedAt).toLocaleString()}</p>
      </div>
    </div>
  );
}

export default JobViewPage;
