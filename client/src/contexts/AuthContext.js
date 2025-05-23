import React, { createContext, useState, useEffect, useCallback } from 'react';
import authService from '../services/authService';
import { useNavigate } from 'react-router-dom'; // Import useNavigate

export const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [currentUser, setCurrentUser] = useState(null);
  const [token, setToken] = useState(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate(); // Hook for navigation

  useEffect(() => {
    const storedToken = authService.getToken();
    const storedUser = authService.getCurrentUser();

    if (storedToken && storedUser) {
      setToken(storedToken);
      setCurrentUser(storedUser);
      setIsAuthenticated(true);
    }
    setIsLoading(false);
  }, []);

  const loginContext = async (username, password) => {
    try {
      const data = await authService.login(username, password);
      setToken(data.token);
      setCurrentUser(data.user);
      setIsAuthenticated(true);
      return data;
    } catch (error) {
      // Clear any lingering state if login fails
      setToken(null);
      setCurrentUser(null);
      setIsAuthenticated(false);
      throw error; // Re-throw to be caught by the calling component
    }
  };

  const registerContext = async (username, password, role) => {
    try {
      const data = await authService.register(username, password, role);
      // Optionally, log in the user directly after registration or redirect to login
      return data;
    } catch (error) {
      throw error; // Re-throw
    }
  };

  const logoutContext = useCallback(() => {
    authService.logout();
    setToken(null);
    setCurrentUser(null);
    setIsAuthenticated(false);
    // Redirect to login page after logout
    // Ensure navigate is available and this component is within Router context
    navigate('/login', { replace: true });
  }, [navigate]);


  const contextValue = {
    currentUser,
    token,
    isAuthenticated,
    isLoading,
    loginContext,
    registerContext,
    logoutContext,
  };

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  );
};

// Custom hook to use the auth context
export const useAuth = () => {
  const context = React.useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
