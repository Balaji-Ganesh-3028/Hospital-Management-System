import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Sidebar.css';

const Sidebar: React.FC = () => {
  const location = useLocation();
  const user = JSON.parse(localStorage.getItem('user') || '{}'); // safer default
  const role = user?.roleName;
  const isPatient = role === 'Patient';
  const isDoctor = role === 'Doctor';
  const isAdmin = role === 'Admin';
  const isFrontDesk = role === 'Front Desk';


  const navItems = [
    { path: '/dashboard', name: 'Dashboard' },
    { path: '/doctors', name: 'Doctors' },
    { path: '/patients', name: 'Patients' },
    { path: '/users', name: 'Users' }, // label for this can be overridden per-role below
    { path: '/appointments', name: 'Appointments' },
    // { path: '/settings', name: 'Settings' },
  ];

  // Filter based on role
  const filteredNavItems = navItems.filter((item) => {
    if (isPatient && item.path === '/doctors') return false; // hide Doctors for Patients
    if (isDoctor && item.path === '/patients') return false; // hide Patients for Doctors
    if (isPatient && item.path === '/appointments') return false;
    if (isPatient && item.path === '/patients') return false;
    if (isDoctor && item.path === '/doctors') return false;
    // keep Users visible for Admin/Doctor etc — we'll change the label below
    return true;
  });

  // Returns the display name for an item depending on the current user's role.
  const getDisplayName = (itemPath: string, defaultName: string) => {
    if (itemPath === '/users') {
      if (isDoctor || isPatient) {
        // show "My Profile" for doctors
        return 'My Profile';
      }
      if (isAdmin || isFrontDesk) {
        // example admin wording — change this string to whatever you prefer
        return 'Manage Users';
      }
      // default for other roles
      return defaultName;
    }
    return defaultName;
  };

  return (
    <aside className="sidebar-container">
      <div className="sidebar-logo">Hospital Management</div>
      <nav className="sidebar-nav">
        <ul>
          {filteredNavItems.map((item) => (
            <li
              key={item.path}
              className={location.pathname === item.path ? 'active' : ''}
            >
              <Link to={item.path}>{getDisplayName(item.path, item.name)}</Link>
            </li>
          ))}
        </ul>
      </nav>
    </aside>
  );
};

export default Sidebar;