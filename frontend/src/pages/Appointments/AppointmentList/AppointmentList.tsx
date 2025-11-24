import React, { useState, useEffect } from 'react';
import './AppointmentList.css';
import { GetAllAppointments } from '../../../Service/Appointment/Appointment';
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import type { AppointmentDetails } from '../../../Models/Appointment';
import { useNavigate } from 'react-router-dom';

interface AddAppointmentProps {
  onAddAppointment: () => void;
}

function AppointmentList({ onAddAppointment }: AddAppointmentProps) {
  const [appointments, setAppointments] = useState<AppointmentDetails[]>([]);
  const [filteredAppointments, setFilteredAppointments] = useState<AppointmentDetails[]>([]);
  const [activeTab, setActiveTab] = useState('All');
  const { showSpinner, hideSpinner } = useSpinner();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchAppointments = async () => {
      showSpinner();
      try {
        const response = await GetAllAppointments();
        if (response.data) {
          setAppointments(response.data);
          setFilteredAppointments(response.data);
        }
      } catch (error) {
        console.error("Error fetching appointments:", error);
      } finally {
        hideSpinner();
      }
    };

    fetchAppointments();
  }, []);

  useEffect(() => {
    if (activeTab === 'All') {
      setFilteredAppointments(appointments);
    } else if (activeTab === 'Today') {
      const today = new Date().toISOString().split('T')[0];
      const todayAppointments = appointments.filter(appointment => appointment.appointmentDetails.appointmentDate.split('T')[0] === today);
      setFilteredAppointments(todayAppointments);
    }
  }, [activeTab, appointments]);

  const navigateToAppointmentProfile = (userId: number) => {
    console.log(userId);
    onAddAppointment();
    navigate(`/appointments?page=addAppointment&action=edit&userId=${userId}`)
  }

  return (
    <div className="appointment-list-container">
      <div className="tabs">
        <button className={`tab ${activeTab === 'All' ? 'active' : ''}`} onClick={() => setActiveTab('All')}>All</button>
        <button className={`tab ${activeTab === 'Today' ? 'active' : ''}`} onClick={() => setActiveTab('Today')}>Today</button>
      </div>
      <div className="table-responsive">
        <table className="app-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Appointment Date</th>
              <th>Doctor Name</th>
              <th>Patient Name</th>
              <th>Purpose of Visit</th>
              <th>Illness/Diseases</th>
              <th>Procedure/Medications</th>
              <th>Current Status</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {filteredAppointments.map((appointment) => (
              <tr key={appointment.appointmentDetails.appointmentId}>
                <td>{appointment.appointmentDetails.appointmentId}</td>
                <td>{appointment.appointmentDetails.appointmentDate}</td>
                <td>{appointment.doctorInfo.doctorFirstName}</td>
                <td>{appointment.patientInfo.patientFirstName}</td>
                <td>{appointment.appointmentDetails.purposeOfVisitName}</td>
                <td>{appointment.appointmentDetails.illnessOrDisease}</td>
                <td>{appointment.appointmentDetails.proceduresOrMedication}</td>
                <td><span className={`status-badge status-${appointment.appointmentDetails.status.toLowerCase()}`}>{appointment.appointmentDetails.status}</span></td>
                <td>
                  {(appointment.appointmentDetails.status === 'Scheduled' || appointment.appointmentDetails.status === 'Rescheduled') && <button className="edit-btn" onClick={() => navigateToAppointmentProfile(appointment.appointmentDetails.appointmentId)}><i className="fas fa-edit"></i></button>}
                  {/* <button className="delete-btn"><i className="fas fa-trash"></i></button> */}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default AppointmentList;
