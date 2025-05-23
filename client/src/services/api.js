import axios from 'axios';
import authService from './authService'; // To get the token

const apiClient = axios.create({
  baseURL: '/api', // Already proxied by setupProxy.js
});

apiClient.interceptors.request.use(
  config => {
    const token = authService.getToken();
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

// Optional: Add a response interceptor for global error handling (e.g., 401 redirects)
apiClient.interceptors.response.use(
  response => response,
  error => {
    if (error.response && error.response.status === 401) {
      // If 401 and not on the login page, redirect to login
      // This handles token expiration or invalid tokens globally
      authService.logout(); // Clear stored token/user
      // Avoid redirect loop if already on login page or if error is from login attempt
      if (window.location.pathname !== '/login' && !error.config.url.endsWith('/login')) {
         // Check if the error is from a non-login related endpoint
        console.warn('Unauthorized access or token expired. Redirecting to login.');
        window.location.href = '/login'; // Force redirect
      }
    }
    return Promise.reject(error);
  }
);


export default apiClient;
