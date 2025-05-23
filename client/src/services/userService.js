import apiClient from './api'; // Assuming apiClient will be created

const getAllUsers = async (params) => {
  try {
    // Example: params = { role: 'mechanic' }
    const response = await apiClient.get('/users', { params }); // Ensure this matches backend
    return response.data;
  } catch (error) {
    console.error('Error fetching users:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch users');
  }
};

const userService = {
  getAllUsers,
  // Add other user-related API calls here
};

export default userService;
