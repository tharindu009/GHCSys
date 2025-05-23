import apiClient from './api'; // Assuming apiClient will be created

const getAllServices = async (params) => {
  try {
    // Example: params = { category: 'maintenance' }
    const response = await apiClient.get('/services', { params }); // Ensure this matches backend
    return response.data; // Expect { services: [], totalPages, currentPage, totalServices } or similar
  } catch (error) {
    console.error('Error fetching services:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch services');
  }
};

const serviceService = {
  getAllServices,
  // Add other service-related API calls here
};

export default serviceService;
