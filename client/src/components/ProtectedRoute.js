import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const ProtectedRoute = ({ element: ComponentElement, allowedRoles, ...rest }) => {
  const { isAuthenticated, isLoading, currentUser } = useAuth();
  const location = useLocation();

  if (isLoading) {
    return <div>Loading application...</div>; // Or a spinner component
  }

  if (!isAuthenticated) {
    // Redirect them to the /login page, but save the current location they were
    // trying to go to when they were redirected. This allows us to send them
    // along to that page after they login, which is a nicer user experience
    // than dropping them off on the home page.
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // If allowedRoles is provided, check if the current user's role is included
  if (allowedRoles && allowedRoles.length > 0) {
    if (!currentUser || !currentUser.role || !allowedRoles.includes(currentUser.role)) {
      // User is authenticated but does not have the required role
      // Redirect to a "not authorized" page or back to dashboard/home
      // For simplicity, redirecting to dashboard.
      // A dedicated "Unauthorized" page would be better UX.
      console.warn(`User role ${currentUser?.role} not authorized for this route.`);
      return <Navigate to="/dashboard" state={{ unauthorized: true, from: location }} replace />;
    }
  }

  return ComponentElement; // Render the passed element
};

export default ProtectedRoute;
