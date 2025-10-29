import React, { useState } from 'react';
import './Appointments.css';
import AppointmentList from './AppointmentList/AppointmentList';
import AddAppointment from './AddAppointment/AddAppointment';

const Appointments: React.FC = () => {
  const [activeTab, setActiveTab] = useState('appointmentList');

  return (
    <div className="app-container">
      <div className="app-tabs">
        <button
          className={activeTab === 'appointmentList' ? 'active' : ''}
          onClick={() => setActiveTab('appointmentList')}
        >
          Appointment List
        </button>
        <button
          className={activeTab === 'addAppointment' ? 'active' : ''}
          onClick={() => setActiveTab('addAppointment')}
        >
          Add Appointment
        </button>
      </div>
      <div className="app-content">
        {activeTab === 'appointmentList' && <AppointmentList />}
        {activeTab === 'addAppointment' && <AddAppointment />}
      </div>
    </div>
  );
};

export default Appointments;
