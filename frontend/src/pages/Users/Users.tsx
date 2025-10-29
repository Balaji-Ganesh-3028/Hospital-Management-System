import React, { useState } from 'react';
import './Users.css';
import UserList from './UserList/UserList';
import UserProfile from '../UserProfile/UserProfile';

const Users: React.FC = () => {
  const [activeTab, setActiveTab] = useState('userList');

  return (
    <div className="users-container">
      <div className="users-tabs">
        <button
          className={activeTab === 'userList' ? 'active' : ''}
          onClick={() => setActiveTab('userList')}
        >
          User List
        </button>
        <button
          className={activeTab === 'userProfile' ? 'active' : ''}
          onClick={() => setActiveTab('userProfile')}
        >
          User Profile
        </button>
      </div>
      <div className="users-content">
        {activeTab === 'userList' && <UserList />}
        {activeTab === 'userProfile' && <UserProfile />}
      </div>
    </div>
  );
};

export default Users;
