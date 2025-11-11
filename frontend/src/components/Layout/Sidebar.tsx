import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Sidebar.css';

const Sidebar: React.FC = () => {
  const location = useLocation();
const user = JSON.parse(localStorage.getItem('user') || '{}'); // safer default
// const isFrontDesk = user?.roleName === 'Front Desk';
const isPatient = user?.roleName === 'Patient';
const isDoctor = user?.roleName === 'Doctor';

  const navItems = [
    { path: '/dashboard', name: 'Dashboard' },
    { path: '/doctors', name: 'Doctors' },
    { path: '/patients', name: 'Patients' },
    { path: '/users', name: 'Users' },
    { path: '/appointments', name: 'Appointments' },
    // { path: '/settings', name: 'Settings' },
  ];

  // Filter based on role
  const filteredNavItems = navItems.filter((item) => {
    if (isPatient && item.path === '/doctors') return false;  // hide Doctors for Patients
    if (isDoctor && item.path === '/patients') return false;  // hide Patients for Doctors
    if (isPatient && item.path === '/appointments') return false;
    if (isPatient && item.path === '/patients') return false;
    if(isDoctor && item.path === '/doctors') return false;
    // you can add more rules for Front Desk here if needed
    return true;
  });

  return (
    <aside className="sidebar-container">
      <div className="sidebar-logo">Hospital Management</div>
      <nav className="sidebar-nav">
        <ul>
          {filteredNavItems.map((item) => (
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
