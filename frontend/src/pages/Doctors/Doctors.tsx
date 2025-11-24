import React, { useEffect } from 'react';
import './Doctors.css';
import DoctorList from './DoctorList/DoctorList';
import AddDoctor from './AddDoctor/AddDoctor';
import { useSearchParams } from 'react-router-dom';

const Doctors: React.FC = () => {
  const [searchParams, setSearchParams] = useSearchParams('doctorList');
  const activeTab = searchParams.get('page') || 'doctorList';

  const  user = JSON.parse(localStorage.getItem('user') || ''); // Use the useAuth hook
  const isFrontDesk = user?.roleName == 'Front Desk'; // Check if the user is Front Desk
  const isPatient = user?.roleName == 'Patient'; // Check if the user is Patient
  const isDoctor = user?.roleName == 'Doctor'; // Check if the user is Patient
  const isAdmin = user?.roleName == 'Admin' || user?.roleName == 'Super Admin'; // Check if the user is Patient

  const switchTab = (tab: string) => {
    setSearchParams({ page: tab });
  };

   useEffect(() => {
      if (isFrontDesk || isPatient) { 
        switchTab('doctorList');
        return;
      }
  
      if (isDoctor) {
        switchTab('addDoctor');
        return;
      }
    }, [])
  
  return (
    <div className="app-container">
      <div className="app-tabs">
        {(isFrontDesk || isPatient || isAdmin) && (<button
          className={activeTab === 'doctorList' ? 'active' : ''}
          onClick={() => switchTab('doctorList')}
        >
          Doctor List
        </button>)}
        {(!isFrontDesk && !isPatient && (isDoctor)) && (
          <button
            className={activeTab === 'addDoctor' ? 'active' : ''}
            onClick={() => switchTab('addDoctor')}
          >
            Add Doctor
          </button>)}
      </div>
      <div className="app-content">
        {activeTab === 'doctorList' && <DoctorList onClickEdit={() => setSearchParams("addDoctor")}/>}
        {activeTab === 'addDoctor' && <AddDoctor />}
      </div>
    </div>
  );
};

export default Doctors;
