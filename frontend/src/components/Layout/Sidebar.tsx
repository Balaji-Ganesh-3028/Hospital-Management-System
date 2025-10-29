import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Sidebar.css';

const Sidebar: React.FC = () => {
  const location = useLocation();

  const navItems = [
    { path: '/dashboard', name: 'Dashboard' },
    { path: '/doctors', name: 'Doctors' },
    { path: '/patients', name: 'Patients' },
    { path: '/users', name: 'Users' },
    { path: '/appointments', name: 'Appointments' },
    { path: '/settings', name: 'Settings' },
  ];

  return (
    <aside className="sidebar-container">
      <div className="sidebar-logo">Hospital Management</div>
      <nav className="sidebar-nav">
        <ul>
          {navItems.map((item) => (
            <li key={item.path} className={location.pathname === item.path ? 'active' : ''}>
              <Link to={item.path}>{item.name}</Link>
            </li>
          ))}
        </ul>
      </nav>
    </aside>
  );
};

export default Sidebar;
