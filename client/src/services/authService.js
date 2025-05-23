import apiClient from './api'; // Use the configured axios client

const API_URL_BASE = '/auth'; // Base path for auth, already part of apiClient's baseURL

const login = async (username, password) => {
  try {
    const response = await apiClient.post(`${API_URL_BASE}/login`, { username, password });
    if (response.data && response.data.token) {
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('user', JSON.stringify(response.data.user));
    }
    return response.data;
  } catch (error) {
    console.error('Login service error:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Login failed');
  }
};

const register = async (username, password, role) => {
  try {
    const response = await apiClient.post(`${API_URL_BASE}/register`, { username, password, role });
    return response.data;
  } catch (error) {
    console.error('Register service error:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Registration failed');
  }
};

const logout = () => {
  localStorage.removeItem('token');
  localStorage.removeItem('user');
  // Note: The redirect is now handled more globally in api.js or by AuthContext
};

const getCurrentUser = () => {
  const userStr = localStorage.getItem('user');
  if (userStr) {
    try {
      return JSON.parse(userStr);
    } catch (e) {
      console.error("Error parsing user from localStorage", e);
      localStorage.removeItem('user');
      localStorage.removeItem('token');
      return null;
    }
  }
  return null;
};

const getToken = () => {
  return localStorage.getItem('token');
};

const authService = {
  login,
  register,
  logout,
  getCurrentUser,
  getToken,
};

export default authService;
