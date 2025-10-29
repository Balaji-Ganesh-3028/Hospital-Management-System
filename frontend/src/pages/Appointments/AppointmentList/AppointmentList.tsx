import React from 'react';
import './AppointmentList.css';

const AppointmentList: React.FC = () => {
  const appointments = [
    {
      id: 1,
      appointmentDate: '2023-10-26',
      doctorName: 'Dr. Emily White',
      purposeOfVisit: 'Routine Checkup',
      illnessDiseases: 'None',
      procedureMedications: 'N/A',
      currentStatus: 'Completed',
    },
    {
      id: 2,
      appointmentDate: '2023-11-01',
      doctorName: 'Dr. David Green',
      purposeOfVisit: 'Follow-up',
      illnessDiseases: 'Flu',
      procedureMedications: 'Tamiflu',
      currentStatus: 'Scheduled',
    },
  ];

  return (
    <div className="appointment-list-container">
      <div className="appointment-list-actions">
        <button><i className="fas fa-plus"></i> Add Appointment</button>
      </div>
      <table className="app-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Appointment Date</th>
            <th>Doctor Name</th>
            <th>Purpose of Visit</th>
            <th>Illness/Diseases</th>
            <th>Procedure/Medications</th>
            <th>Current Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {appointments.map((appointment) => (
            <tr key={appointment.id}>
              <td>{appointment.id}</td>
              <td>{appointment.appointmentDate}</td>
              <td>{appointment.doctorName}</td>
              <td>{appointment.purposeOfVisit}</td>
              <td>{appointment.illnessDiseases}</td>
              <td>{appointment.procedureMedications}</td>
              <td>{appointment.currentStatus}</td>
              <td>
                <button className="edit-btn"><i className="fas fa-edit"></i></button>
                <button className="delete-btn"><i className="fas fa-trash"></i></button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AppointmentList;
