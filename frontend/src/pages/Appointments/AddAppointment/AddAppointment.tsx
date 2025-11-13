import React, { useState, useEffect } from 'react';
import './AddAppointment.css';
import { GetAllDoctors } from '../../../Service/Doctor/Doctor';
import { GetAllPatients } from '../../../Service/Patient/Patient';
import type { DoctorDetails } from '../../../Models/Doctor';
import type { PatientDetails } from '../../../Models/Patient';
import { GetAppointmentDetails, InsertAppointment, UpdateAppointment } from '../../../Service/Appointment/Appointment';
import { ToastMessageTypes } from '../../../Enums/Toast-Message';
import { notify } from '../../../Service/Toast-Message/Toast-Message';
import type { Lookup } from '../../../Models/Lookups';
import { GetAppointmentStatus, GetAppointmentType } from '../../../Service/Lookups/Lookups';
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import { useSearchParams } from 'react-router-dom';

interface AddAppointmentProps {
  onAddAppointment: () => void;
}
function AddAppointment({ onAddAppointment }: AddAppointmentProps) {
  const [searchParams] = useSearchParams('appointmentList');
  // const action = searchParams.get('action');
  const appointmentId = searchParams.get('userId');

  const [formData, setFormData] = useState({
    patientId: '',
    doctorId: '',
    appointmentDate: '',
    purposeOfVisit: '',
    illnessDiseases: '',
    procedureMedications: '',
    currentStatus: '',
  });

  const [doctors, setDoctors] = useState<DoctorDetails[]>([]);
  const [patients, setPatients] = useState<PatientDetails[]>([]);
  const [appointmentType, setAppointmentType] = useState<Lookup[]>([]);
  const [appointmentStatus, setAppointmentStatus] = useState<Lookup[]>([]);

  const today = new Date();
  const year = today.getFullYear();
  const month = (today.getMonth() + 1).toString().padStart(2, '0');
  const day = today.getDate().toString().padStart(2, '0');
  const minDate = `${year}-${month}-${day}`;

  const { showSpinner, hideSpinner } = useSpinner();
  
  useEffect(() => {
    const fetchDoctorsAndPatients = async () => {
      try {
        showSpinner();
        const doctorsResponse = await GetAllDoctors();
        setDoctors(doctorsResponse.data);

        const patientsResponse = await GetAllPatients();
        setPatients(patientsResponse);

        const appointmentTypeResponse = await GetAppointmentType();
        setAppointmentType(appointmentTypeResponse);

        const appointmentStatusResponse = await GetAppointmentStatus();
        setAppointmentStatus(appointmentStatusResponse);
        setTimeout(() => {
          hideSpinner();
        }, 500)
      } catch (error) {
        console.error('Error fetching doctors or patients:', error);
        hideSpinner();
      }
    };

    fetchDoctorsAndPatients();
    if(appointmentId) onLoadAppointmentDetails(appointmentId);
  }, []);

  const onLoadAppointmentDetails = async (appointmentId: string) => {
    try {
      const response = await GetAppointmentDetails(Number(appointmentId));
      console.log(response.data);
      setFormData({
        patientId: response.data.patientId,
        doctorId:  response.data.doctorId,
        appointmentDate: response.data.appointmentDate,
        purposeOfVisit: response.data.purposeOfVisit,
        illnessDiseases: response.data.illnessOrDisease,
        procedureMedications: response.data.proceduresOrMedication,
        currentStatus: response.data.currentStatus,
      });
      

    } catch (error) {
      console.log(error);
    }
    
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    showSpinner();
    console.log('Appointment Data:', formData);
    // Here you would typically send the data to your backend
    const payload = {
      id: Number(appointmentId) || 0,
      patientId: Number(formData.patientId),
      doctorId: Number(formData.doctorId),
      appointmentDate: formData.appointmentDate,
      purposeOfVisit: Number(formData.purposeOfVisit),
      illnessOrDisease: formData.illnessDiseases,
      proceduresOrMedication: formData.procedureMedications,
      currentStatus: Number(formData.currentStatus),
    };

    if (appointmentId) {
      try {
        const response = await UpdateAppointment(payload);
        if (response) {
          notify(response.data, ToastMessageTypes.SUCCESS);
          onAddAppointment();
          setTimeout(() => {
            hideSpinner();
          }, 500)
        } 
      } catch (error) {
        console.error('Error inserting patient:', error);
        notify("Something went wrong!!!", ToastMessageTypes.ERROR)
      }
      return;
    }

    const response = await InsertAppointment(payload)

    if (response) {
      notify(response.data, ToastMessageTypes.SUCCESS);
      onAddAppointment();
      setTimeout(() => {
        hideSpinner();
      }, 500)
    } 
  };

  return (
    <div className="add-appointment-container">
      {appointmentId ? <h2>Edit Appointment</h2> : <h2>Add New Appointment</h2>}
      <form onSubmit={handleSubmit} className="add-appointment-form">
        <div className="form-row">
          <div className="form-group">
            <label htmlFor="patientId">Patient:</label>
            <select
              id="patientId"
              name="patientId"
              value={formData.patientId}
              onChange={handleChange}
              required
            >
              <option value="">Select Patient</option>
              {patients.map((patient) => (
                <option key={patient.patientId} value={patient.patientId}>
                  {patient.firstName} {patient.lastName}
                </option>
              ))}
            </select>
          </div>
          <div className="form-group">
            <label htmlFor="doctorId">Doctor:</label>
            <select
              id="doctorId"
              name="doctorId"
              value={formData.doctorId}
              onChange={handleChange}
              required
            >
              <option value="">Select Doctor</option>
              {doctors.map((doctor) => (
                <option key={doctor.doctorId} value={doctor.doctorId}>
                  {doctor.firstName} {doctor.lastName}
                </option>
              ))}
            </select>
          </div>
        </div>
        <div className="form-group">
          <label htmlFor="appointmentDate">Appointment Date:</label>
          <input
            type="date"
            id="appointmentDate"
            name="appointmentDate"
            value={formData.appointmentDate}
            onChange={handleChange}
            min={minDate}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="purposeOfVisit">Purpose of Visit:</label>
           <select
            id="purposeOfVisit"
            name="purposeOfVisit"
            value={formData.purposeOfVisit}
            onChange={handleChange}
            required
          >
            <option value="">Select Status</option>
            ({appointmentType.map((status) => (
            <option key={status.id} value={status.id}>{status.name}</option>  
            ))})
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="illnessDiseases">Illness/Diseases:</label>
          <input
            type="text"
            id="illnessDiseases"
            name="illnessDiseases"
            value={formData.illnessDiseases}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label htmlFor="procedureMedications">Procedure/Medications:</label>
          <input
            type="text"
            id="procedureMedications"
            name="procedureMedications"
            value={formData.procedureMedications}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label htmlFor="currentStatus">Current Status:</label>
          <select
            id="currentStatus"
            name="currentStatus"
            value={formData.currentStatus}
            onChange={handleChange}
            required
          >
            <option value="">Select Status</option>
            ({appointmentStatus.map((status) => (
            <option key={status.id} value={status.id}>{status.name}</option>  
            ))})
          </select>
        </div>
        <button type="submit" className="submit-button">Save Appointment</button>
      </form>
    </div>
  );
};

export default AddAppointment;


