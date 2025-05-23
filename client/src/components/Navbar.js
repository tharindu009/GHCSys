import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import '../assets/css/Navbar.css'; // Styles for the navbar

function Navbar() {
  const { currentUser, logoutContext, isAuthenticated } = useAuth();

  return (
    <nav className="navbar">
      <div className="navbar-brand">
        <Link to={isAuthenticated ? "/dashboard" : "/"} className="navbar-logo">
          AutoShop MS
        </Link>
      </div>
      <div className="navbar-links">
        {isAuthenticated && currentUser ? (
          <>
            <span className="navbar-user-greeting">
              Hello, {currentUser.username} ({currentUser.role})
            </span>
            <button onClick={logoutContext} className="navbar-button logout-button">
              Logout
            </button>
          </>
        ) : (
          <>
            <Link to="/login" className="navbar-link">Login</Link>
            <Link to="/register" className="navbar-link">Register</Link>
          </>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
