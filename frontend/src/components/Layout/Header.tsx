import React from 'react';
import './Header.css';

const Header: React.FC = () => {
  return (
    <header className="header-container">
      <div className="header-search">
      </div>
      <div className="header-profile">
        <span className="profile-name">John Doe</span>
        {/* <img src="https://via.placeholder.com/40" alt="Profile" /> */}
      </div>
    </header>
  );
};

export default Header;
