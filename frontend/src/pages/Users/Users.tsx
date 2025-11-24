import React, { useEffect } from 'react';
import './Users.css';
import UserList from './UserList/UserList';
import UserProfile from './UserProfile/UserProfile';
import { useSearchParams } from 'react-router-dom';
import AddPatient from '../Patients/AddPatient/AddPatient';
import AddDoctor from '../Doctors/AddDoctor/AddDoctor';

const Users: React.FC = () => {
  const [searchParams, setSearchParams] = useSearchParams('userList');
  const  user = JSON.parse(localStorage.getItem('user') || ''); // Use the useAuth hook
  const isFrontDesk = user?.roleName == 'Front Desk'; // Check if the user is Front Desk
  const isPatient = user?.roleName == 'Patient'; // Check if the user is Patient
  const isDoctor = user?.roleName == 'Doctor'; // Check if the user is Patient
  const isAdmin = user?.roleName == 'Admin' || user?.roleName == 'Super Admin'; // Check if the user is Patient

  const activeTab = searchParams.get('page') || 'userList';

  const switchTab = (tab: string) => {
    setSearchParams({ page: tab });
  };

  useEffect(() => {
    if (isFrontDesk) { 
      switchTab('userList');
      return;
    }

    if (isPatient || isDoctor) {
      switchTab('userProfile');
      return;
    }
  }, [])


  return (
  <>
      {(isFrontDesk || isAdmin) && (<div className="app-container">
        {(isFrontDesk || isAdmin) && (<div className="app-tabs">
          {(isFrontDesk || isAdmin) && (<button
            className={activeTab === 'userList' ? 'active' : ''}
            onClick={() => switchTab('userList')}
          >
            User List
          </button>)}
          {(isAdmin) && ( // Conditionally render the button
            <button
              className={`${isAdmin ? 'add-user-profile-button' : ''} ${activeTab === 'userProfile' ? 'active' : ''}`}
              onClick={() => switchTab('userProfile')}
            >
              User Profile
            </button>
          )}
        </div>)}
        <div className="app-content">
          {activeTab === 'userList' && <UserList onClickEdit={() => setSearchParams("userProfile")} />}
          {activeTab === 'userProfile' && <UserProfile />}
        </div>
      </div>)}

      {(isPatient || isDoctor) && (<div className="app-container">
        <div className="app-tabs">
          {(isPatient || isDoctor) && (<button
            className={activeTab === 'userProfile' ? 'active' : ''}
            onClick={() => switchTab('userProfile')}
          >
            User Profile
          </button>)}
          {(isPatient) && ( // Conditionally render the button
            <button
              className={activeTab === 'PatientDetails' ? 'active' : ''}
              onClick={() => switchTab('PatientDetails')}
            >
              Patient Details
            </button>
          )}
          {(isDoctor) && ( // Conditionally render the button
            <button
              className={activeTab === 'DoctorDetails' ? 'active' : ''}
              onClick={() => switchTab('DoctorDetails')}
            >
              Doctor Details
            </button>
          )}
        </div>
        <div className="app-content">
          {/* {activeTab === 'userList' && <UserList onClickEdit={() => setSearchParams("userProfile")} />} */}
          {activeTab === 'userProfile' && <UserProfile/>}
          {activeTab === 'PatientDetails' && <AddPatient />}
          {activeTab === 'DoctorDetails' && <AddDoctor />}
        </div>
      </div>)}
    </>  
  );
};

export default Users;
