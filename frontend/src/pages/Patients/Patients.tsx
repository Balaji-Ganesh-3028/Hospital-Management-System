import React, { useState } from 'react';
import './Patients.css';
import PatientList from './PatientList/PatientList';
import AddPatient from './AddPatient/AddPatient';

const Patients: React.FC = () => {
  const [activeTab, setActiveTab] = useState('patientList');

  return (
    <div className="app-container">
      <div className="app-tabs">
        <button
          className={activeTab === 'patientList' ? 'active' : ''}
          onClick={() => setActiveTab('patientList')}
        >
          Patient List
        </button>
        <button
          className={activeTab === 'addPatient' ? 'active' : ''}
          onClick={() => setActiveTab('addPatient')}
        >
          Add Patient
        </button>
      </div>
      <div className="app-content">
        {activeTab === 'patientList' && <PatientList />}
        {activeTab === 'addPatient' && <AddPatient />}
      </div>
    </div>
  );
};

export default Patients;
