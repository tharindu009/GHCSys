import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';
import Sidebar from './Sidebar';
import '../assets/css/Layout.css'; // Styles for the layout

function Layout() {
  return (
    <div className="app-layout">
      <Navbar />
      <div className="main-content-area">
        <Sidebar />
        <main className="content">
          <Outlet /> {/* Nested route components will render here */}
        </main>
      </div>
    </div>
  );
}

export default Layout;
