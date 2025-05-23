import apiClient from './api'; // Assuming apiClient is set up

const getAllCustomers = async () => {
  try {
    const response = await apiClient.get('/customers'); // Ensure this matches backend
    return response.data;
  } catch (error) {
    console.error('Error fetching customers:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch customers');
  }
};

const customerService = {
  getAllCustomers,
  // Add other customer-related API calls here (e.g., getCustomerById, createCustomer)
};

export default customerService;
