import apiClient from './api';

const createJob = async (jobData) => {
  try {
    const response = await apiClient.post('/jobs', jobData);
    return response.data;
  } catch (error) {
    console.error('Error creating job:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to create job');
  }
};

const getAllJobs = async (filters) => {
  try {
    const response = await apiClient.get('/jobs', { params: filters });
    return response.data; // Expects { jobs: [], totalPages, currentPage, totalJobs }
  } catch (error) {
    console.error('Error fetching jobs:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch jobs');
  }
};

const getJobById = async (jobId) => {
  try {
    const response = await apiClient.get(`/jobs/${jobId}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching job ${jobId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch job details');
  }
};

const updateJob = async (jobId, jobData) => {
  try {
    const response = await apiClient.put(`/jobs/${jobId}`, jobData);
    return response.data;
  } catch (error) {
    console.error(`Error updating job ${jobId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to update job');
  }
};

const deleteJob = async (jobId) => {
  try {
    const response = await apiClient.delete(`/jobs/${jobId}`);
    return response.data;
  } catch (error) {
    console.error(`Error deleting job ${jobId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to delete job');
  }
};

const jobService = {
  createJob,
  getAllJobs,
  getJobById,
  updateJob,
  deleteJob,
};

export default jobService;
