import React, { useState } from 'react';
import './Doctors.css';
import DoctorList from './DoctorList/DoctorList';
import AddDoctor from './AddDoctor/AddDoctor';

const Doctors: React.FC = () => {
  const [activeTab, setActiveTab] = useState('doctorList');

  return (
    <div className="app-container">
      <div className="app-tabs">
        <button
          className={activeTab === 'doctorList' ? 'active' : ''}
          onClick={() => setActiveTab('doctorList')}
        >
          Doctor List
        </button>
        <button
          className={activeTab === 'addDoctor' ? 'active' : ''}
          onClick={() => setActiveTab('addDoctor')}
        >
          Add Doctor
        </button>
      </div>
      <div className="app-content">
        {activeTab === 'doctorList' && <DoctorList />}
        {activeTab === 'addDoctor' && <AddDoctor />}
      </div>
    </div>
  );
};

export default Doctors;
