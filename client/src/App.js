import React from 'react';
import { Routes, Route } from 'react-router-dom'; 

import { useAuth } from './contexts/AuthContext';

// Layout and Pages
import Layout from './components/Layout';
import ProtectedRoute from './components/ProtectedRoute';
import HomePage from './pages/HomePage'; 
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import DashboardPage from './pages/DashboardPage';
import JobsPage from './pages/JobsPage';
import JobFormPage from './pages/JobFormPage';
import JobViewPage from './pages/JobViewPage';  
import InvoicesPage from './pages/InvoicesPage'; 
import InvoiceViewPage from './pages/InvoiceViewPage';
import InventoryPage from './pages/InventoryPage';
import InventoryItemFormPage from './pages/InventoryItemFormPage'; // Added InventoryItemFormPage
import CustomersPage from './pages/CustomersPage'; 
import VehiclesPage from './pages/VehiclesPage'; 
import AdminUsersPage from './pages/AdminUsersPage';
// import NotFoundPage from './pages/NotFoundPage';

import './assets/css/App.css';

function App() {
  const { isLoading } = useAuth(); 

  if (isLoading) {
    return <div style={{textAlign: 'center', marginTop: '50px', fontSize: '1.2em'}}>Loading application...</div>;
  }

  return (
    <Routes>
      {/* Public routes */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/home" element={<HomePage />} /> 
      
      <Route 
        path="/" 
        element={
          <ProtectedRoute 
            element={<Layout />} 
          />
        }
      >
        <Route index element={<ProtectedRoute element={<DashboardPage />} />} /> 
        <Route path="dashboard" element={<ProtectedRoute element={<DashboardPage />} />} />
        
        {/* Job Routes */}
        <Route path="jobs" element={<ProtectedRoute element={<JobsPage />} allowedRoles={['admin', 'service_advisor', 'mechanic']} />} />
        <Route path="jobs/new" element={<ProtectedRoute element={<JobFormPage />} allowedRoles={['admin', 'service_advisor']} />} />
        <Route path="jobs/:id/edit" element={<ProtectedRoute element={<JobFormPage />} allowedRoles={['admin', 'service_advisor']} />} />
        <Route path="jobs/:id" element={<ProtectedRoute element={<JobViewPage />} allowedRoles={['admin', 'service_advisor', 'mechanic']} />} />

        {/* Invoice Routes */}
        <Route path="invoices" element={<ProtectedRoute element={<InvoicesPage />} allowedRoles={['admin', 'service_advisor']} />} />
        <Route path="invoices/:id" element={<ProtectedRoute element={<InvoiceViewPage />} allowedRoles={['admin', 'service_advisor']} />} />

        {/* Inventory Routes */}
        <Route path="inventory" element={<ProtectedRoute element={<InventoryPage />} allowedRoles={['admin', 'service_advisor', 'mechanic']} />} />
        <Route path="inventory/new" element={<ProtectedRoute element={<InventoryItemFormPage />} allowedRoles={['admin', 'service_advisor']} />} />
        <Route path="inventory/:id/edit" element={<ProtectedRoute element={<InventoryItemFormPage />} allowedRoles={['admin', 'service_advisor']} />} />
        {/* Optional: <Route path="inventory/:id" element={<ProtectedRoute element={<InventoryItemViewPage />} allowedRoles={['admin', 'service_advisor', 'mechanic']}/>} /> */}


        <Route path="customers" element={<ProtectedRoute element={<CustomersPage />} allowedRoles={['admin', 'service_advisor']} />} />
        <Route path="vehicles" element={<ProtectedRoute element={<VehiclesPage />} allowedRoles={['admin', 'service_advisor']} />} />
        
        <Route 
          path="admin/users" 
          element={
            <ProtectedRoute 
              element={<AdminUsersPage />} 
              allowedRoles={['admin']} 
            />
          } 
        />
        {/* <Route path="*" element={<NotFoundPage />} /> */} 
      </Route>
      
      {/* <Route path="*" element={<NotFoundPage />} /> */}
    </Routes>
  );
}

export default App;
