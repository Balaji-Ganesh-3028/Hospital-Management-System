import React from 'react';
import './Sidebar.css';

const Sidebar: React.FC = () => {
  return (
    <aside className="sidebar-container">
      <div className="sidebar-logo">Hospital Management</div>
      <nav className="sidebar-nav">
        <ul>
          <li className="active"><a href="/dashboard">Dashboard</a></li>
          <li><a href="/patients">Patients</a></li>
          <li><a href="/appointments">Appointments</a></li>
          <li><a href="/doctors">Doctors</a></li>
          <li><a href="/departments">Departments</a></li>
          <li><a href="/users">Users</a></li>
          <li><a href="/settings">Settings</a></li>
        </ul>
      </nav>
    </aside>
  );
};

export default Sidebar;
