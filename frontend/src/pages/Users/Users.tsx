import React, {  } from 'react';
import './Users.css';
import UserList from './UserList/UserList';
import UserProfile from './UserProfile/UserProfile';
import { useSearchParams } from 'react-router-dom';

const Users: React.FC = () => {
  const [searchParams, setSearchParams] = useSearchParams('userList');

  const activeTab = searchParams.get('page') || 'userList';

   const switchTab = (tab: string) => {
    setSearchParams({ page: tab });
  };


  return (
    <div className="app-container">
      <div className="app-tabs">
        <button
          className={activeTab === 'userList' ? 'active' : ''}
          onClick={() => switchTab('userList')}
        >
          User List
        </button>
        <button
          className={activeTab === 'userProfile' ? 'active' : ''}
          onClick={() => switchTab('userProfile')}
        >
          User Profile
        </button>
      </div>
      <div className="app-content">
        {activeTab === 'userList' && <UserList onClickEdit={() => setSearchParams("userProfile")}/>}
        {activeTab === 'userProfile' && <UserProfile />}
      </div>
    </div>
  );
};

export default Users;
