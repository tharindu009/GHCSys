import apiClient from './api'; // Assuming apiClient will be created

const getVehiclesByCustomerId = async (customerId) => {
  if (!customerId) {
    // Return an empty array or throw an error if customerId is not provided,
    // as fetching vehicles without a customer context might not be desired.
    console.warn('No customerId provided to getVehiclesByCustomerId');
    return []; 
  }
  try {
    const response = await apiClient.get(`/vehicles?customerId=${customerId}`); // Ensure this matches backend
    return response.data;
  } catch (error) {
    console.error(`Error fetching vehicles for customer ${customerId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch vehicles');
  }
};

const vehicleService = {
  getVehiclesByCustomerId,
  // Add other vehicle-related API calls here
};

export default vehicleService;
