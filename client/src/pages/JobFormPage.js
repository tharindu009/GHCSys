import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import JobForm from '../components/jobs/JobForm';
import jobService from '../services/jobService'; // To fetch job data for editing

function JobFormPage() {
  const { id: jobId } = useParams(); // Get jobId from URL if present
  const [jobData, setJobData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const isEditMode = Boolean(jobId);

  useEffect(() => {
    if (isEditMode) {
      setLoading(true);
      setError('');
      jobService.getJobById(jobId)
        .then(data => {
          setJobData(data);
        })
        .catch(err => {
          setError(`Failed to fetch job details: ${err.message}`);
          console.error(err);
        })
        .finally(() => setLoading(false));
    }
  }, [jobId, isEditMode]);

  if (loading && isEditMode) return <p>Loading job data for editing...</p>;
  if (error) return <p className="error-message">Error: {error}</p>;
  if (isEditMode && !jobData && !loading) return <p>Job not found.</p>; // If done loading and no data for edit

  return (
    <div className="job-form-page">
      <h1>{isEditMode ? 'Edit Job' : 'Create New Job'}</h1>
      <JobForm jobData={jobData} isEditMode={isEditMode} />
    </div>
  );
}

export default JobFormPage;
