import React, { useState } from 'react';
import './AddAppointment.css';

const AddAppointment: React.FC = () => {
  const [formData, setFormData] = useState({
    appointmentDate: '',
    doctorName: '',
    purposeOfVisit: '',
    illnessDiseases: '',
    procedureMedications: '',
    currentStatus: '',
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Appointment Data:', formData);
    // Here you would typically send the data to your backend
    alert('Appointment added (check console for data)');
  };

  return (
    <div className="add-appointment-container">
      <h2>Add New Appointment</h2>
      <form onSubmit={handleSubmit} className="add-appointment-form">
        <div className="form-group">
          <label htmlFor="appointmentDate">Appointment Date:</label>
          <input
            type="date"
            id="appointmentDate"
            name="appointmentDate"
            value={formData.appointmentDate}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="doctorName">Doctor Name:</label>
          <input
            type="text"
            id="doctorName"
            name="doctorName"
            value={formData.doctorName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="purposeOfVisit">Purpose of Visit:</label>
          <input
            type="text"
            id="purposeOfVisit"
            name="purposeOfVisit"
            value={formData.purposeOfVisit}
            onChange={handleChange}
            required
          />
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
            <option value="Scheduled">Scheduled</option>
            <option value="Completed">Completed</option>
            <option value="Cancelled">Cancelled</option>
          </select>
        </div>
        <button type="submit" className="submit-button">Add Appointment</button>
      </form>
    </div>
  );
};

export default AddAppointment;
