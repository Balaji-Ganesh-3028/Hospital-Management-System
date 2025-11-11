import React, { useState } from 'react';
import './Header.css';
import { useAuth } from '../../Contexts/useAuth';

const Header: React.FC = () => {
  const { user, logout } = useAuth();
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const getInitials = (name: string) => {
    if (!name) return '';
    const nameParts = name.split(' ');
    if (nameParts.length > 1) {
      return `${nameParts[0][0]}${nameParts[1][0]}`.toUpperCase();
    }
    return name[0].toUpperCase();
  };

  return (
    <header className="header-container">
      <div className="header-search">
      </div>
      <div className="header-profile">
        <div className="profile-icon" onClick={() => setDropdownOpen(!dropdownOpen)}>
          {user ? getInitials(user?.userName) : ''}
        </div>
        {dropdownOpen && (
          <div className="profile-dropdown">
            <div className="dropdown-item">{user?.userName}</div>
            <div className="dropdown-item">{user?.roleName}</div>
            <div className="dropdown-item" onClick={logout}>
              Logout
            </div>
          </div>
        )}
      </div>
    </header>
  );
};

export default Header;
