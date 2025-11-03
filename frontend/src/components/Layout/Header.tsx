import React, { useState } from 'react';
import './Header.css';
import { useAuth } from '../../Contexts/AuthContext';

const Header: React.FC = () => {
  const { user, logout } = useAuth();
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const getInitials = (name: string) => {
    console.log(name);
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
          {user ? getInitials(user.data?.userName) : ''}
        </div>
        {dropdownOpen && (
          <div className="profile-dropdown">
            <div className="dropdown-item">{user?.data?.userName}</div>
            <div className="dropdown-item">{user?.data?.roleName}</div>
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
