import React from 'react';
import { NavLink } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import '../assets/css/Sidebar.css'; // Styles for the sidebar

function Sidebar() {
  const { currentUser } = useAuth();

  if (!currentUser) {
    return null; // Don't show sidebar if not logged in or user data is not available
  }

  return (
    <aside className="sidebar">
      <nav>
        <ul>
          <li><NavLink to="/dashboard" className={({isActive}) => isActive ? "sidebar-link active" : "sidebar-link"}>Dashboard</NavLink></li>
          <li><NavLink to="/jobs" className={({isActive}) => isActive ? "sidebar-link active" : "sidebar-link"}>Jobs</NavLink></li>
          <li><NavLink to="/invoices" className={({isActive}) => isActive ? "sidebar-link active" : "sidebar-link"}>Invoices</NavLink></li>
          <li><NavLink to="/inventory" className={({isActive}) => isActive ? "sidebar-link active" : "sidebar-link"}>Inventory</NavLink></li>
          <li><NavLink to="/customers" className={({isActive}) => isActive ? "sidebar-link active" : "sidebar-link"}>Customers</NavLink></li>
          <li><NavLink to="/vehicles" className={({isActive}) => isActive ? "sidebar-link active" : "sidebar-link"}>Vehicles</NavLink></li>
          
          {/* Example of role-based link */}
          {currentUser.role === 'admin' && (
            <li><NavLink to="/admin/users" className={({isActive}) => isActive ? "sidebar-link active" : "sidebar-link"}>User Management</NavLink></li>
          )}
        </ul>
      </nav>
    </aside>
  );
}

export default Sidebar;
