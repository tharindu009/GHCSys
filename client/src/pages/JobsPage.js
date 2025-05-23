import React, { useState, useEffect, useCallback } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import jobService from '../services/jobService';
import '../assets/css/Jobs.css'; // Create this file later for styling

function JobsPage() {
  const [jobs, setJobs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [filters, setFilters] = useState({ status: '', customerSearch: '' }); // customerSearch for client-side
  const navigate = useNavigate();

  const fetchJobs = useCallback(async () => {
    setLoading(true);
    setError('');
    try {
      const params = {
        page: currentPage,
        limit: 10,
        status: filters.status || undefined,
        // If backend supports customer name search directly, add it here:
        // customerName: filters.customerSearch || undefined, 
      };
      const data = await jobService.getAllJobs(params);
      setJobs(data.jobs || []);
      setTotalPages(data.totalPages || 1);
    } catch (err) {
      setError(err.message || 'Failed to fetch jobs.');
    } finally {
      setLoading(false);
    }
  }, [currentPage, filters.status]); // customerSearch removed from deps if it's client-side only

  useEffect(() => {
    fetchJobs();
  }, [fetchJobs]);

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters(prev => ({ ...prev, [name]: value }));
    setCurrentPage(1); 
  };
  
  // Client-side filtering for customer name - use if backend doesn't support it.
  // For better performance on large datasets, backend search is preferred.
  const displayedJobs = filters.customerSearch 
    ? jobs.filter(job => 
        job.customer && job.customer.name && 
        job.customer.name.toLowerCase().includes(filters.customerSearch.toLowerCase())
      )
    : jobs;


  const handleDelete = async (jobId) => {
    if (window.confirm('Are you sure you want to delete this job?')) {
      try {
        await jobService.deleteJob(jobId);
        // Refetch jobs after deletion to get the updated list and pagination
        fetchJobs(); 
      } catch (err) {
        setError(err.message || 'Failed to delete job.');
      }
    }
  };

  if (loading) return <div className="loading-message">Loading jobs...</div>;
  if (error) return <div className="error-message">Error: {error}</div>;

  return (
    <div className="jobs-page">
      <h1>Job Management</h1>
      <div className="actions-bar">
        <Link to="/jobs/new" className="btn btn-primary">Create New Job</Link>
      </div>

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
          name="status" 
          value={filters.status} 
          onChange={handleFilterChange}
          className="filter-select"
        >
          <option value="">All Statuses</option>
          <option value="Pending">Pending</option>
          <option value="Scheduled">Scheduled</option>
          <option value="In Progress">In Progress</option>
          <option value="Awaiting Parts">Awaiting Parts</option>
          <option value="Completed">Completed</option>
          <option value="Invoiced">Invoiced</option>
          <option value="Cancelled">Cancelled</option>
        </select>
      </div>

      <table className="jobs-table">
        <thead>
          <tr>
            <th>Job ID</th>
            <th>Customer</th>
            <th>Vehicle</th>
            <th>Date</th>
            <th>Status</th>
            <th>Assigned Mechanic</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {displayedJobs.length > 0 ? displayedJobs.map(job => (
            <tr key={job._id}>
              <td>{job.invoiceNumber || job._id.substring(0,8) + "..."}</td>
              <td>{job.customer ? job.customer.name : 'N/A'}</td>
              <td>{job.vehicle ? `${job.vehicle.make} ${job.vehicle.model}` : 'N/A'}</td>
              <td>{new Date(job.jobDate).toLocaleDateString()}</td>
              <td><span className={`status-badge status-${job.status?.toLowerCase().replace(/\s+/g, '-')}`}>{job.status}</span></td>
              <td>{job.assignedMechanic ? job.assignedMechanic.username : 'Unassigned'}</td>
              <td>
                <button onClick={() => navigate(`/jobs/${job._id}`)} className="btn btn-sm btn-info">View</button>
                <button onClick={() => navigate(`/jobs/${job._id}/edit`)} className="btn btn-sm btn-warning">Edit</button>
                <button onClick={() => handleDelete(job._id)} className="btn btn-sm btn-danger">Delete</button>
              </td>
            </tr>
          )) : (
            <tr>
              <td colSpan="7">No jobs found matching your criteria.</td>
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

export default JobsPage;
