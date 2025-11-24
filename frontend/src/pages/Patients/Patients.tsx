import React, { useEffect } from 'react';
import './Patients.css';
import PatientList from './PatientList/PatientList';
import AddPatient from './AddPatient/AddPatient';
import { useSearchParams } from 'react-router-dom';

const Patients: React.FC = () => {
  const [searchParams, setSearchParams] = useSearchParams('patientList');
  const  user = JSON.parse(localStorage.getItem('user') || ''); // Use the useAuth hook
  const isFrontDesk = user?.roleName == 'Front Desk'; // Check if the user is Front Desk
  const isPatient = user?.roleName == 'Patient'; // Check if the user is Patient
  const isAdmin = user?.roleName == 'Admin'; // Check if the user is Admin
  // const isUser = user?.roleName == 'Patient'; // Check if the user is Patient

  const activeTab = searchParams.get('page') || 'patientList';

  const switchTab = (tab: string) => {
    setSearchParams({ page: tab });
  };

  useEffect(() => {
    if (isPatient) {
      switchTab('addPatient');
      return;
    }
  }, [])

  return (
    <div className="app-container">
      <div className="app-tabs">
        {!isPatient && (<button
          className={activeTab === 'patientList' ? 'active' : ''}
          onClick={() => switchTab('patientList')}
        >
          Patient List
        </button>)}
        {!isFrontDesk && (isPatient || isAdmin) && ( // Conditionally render the button
          <button
            className={`${isAdmin ? 'add-patient-button' : ''} ${activeTab === 'addPatient' ? 'active' : ''}`}
            onClick={() => switchTab('addPatient')}
          >
            Add Patient
          </button>
        )}
      </div>
      <div className="app-content">
        {activeTab === 'patientList' && <PatientList onClickEdit={() => setSearchParams("addPatient")}/>}
        {activeTab === 'addPatient' && <AddPatient />}
      </div>
    </div>
  );
};

export default Patients;
