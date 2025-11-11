import React from 'react';
import './Appointments.css';
import AppointmentList from './AppointmentList/AppointmentList';
import AddAppointment from './AddAppointment/AddAppointment';
import { useSearchParams } from 'react-router-dom';

const Appointments: React.FC = () => {
  const [searchParams, setSearchParams] = useSearchParams('appointmentList');
  const activeTab = searchParams.get('page') || 'appointmentList';
  const appointmentId = searchParams.get('userId');
  
   const switchTab = (tab: string) => {
    setSearchParams({ page: tab });
  };

  return (
    <div className="app-container">
      <div className="app-tabs">
        <button
          className={activeTab === 'appointmentList' ? 'active' : ''}
          onClick={() => switchTab('appointmentList')}
        >
          Appointment List
        </button>
        <button
          className={activeTab === 'addAppointment' ? 'active' : ''}
          onClick={() => switchTab('addAppointment')}
        >
         {appointmentId ? <span> Edit Appointment</span> : <span> Add Appointment</span>}
        </button>
      </div>
      <div className="app-content">
        {activeTab === 'appointmentList' && <AppointmentList onAddAppointment={() => switchTab('addAppointment')}/>}
        {activeTab === 'addAppointment' && <AddAppointment onAddAppointment={() => switchTab('appointmentList')}/>}
      </div>
    </div>
  );
};

export default Appointments;
