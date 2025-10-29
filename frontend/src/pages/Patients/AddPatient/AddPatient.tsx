import React, { useState } from 'react';
import './AddPatient.css';

const AddPatient: React.FC = () => {
  const today = new Date().toISOString().split('T')[0];

  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    doj: today,
    bloodGroup: '',
    allergies: '',
    chronicDiseases: '',
    emergencyContactName: '',
    emergencyContactNumber: '',
    insuranceProvider: '',
    insuranceNumber: '',
    medicalHistoryNotes: '',
  });

  const bloodGroups = ['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'];

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Patient Data:', formData);
    // Here you would typically send the data to your backend
    alert('Patient added (check console for data)');
  };

  return (
    <div className="add-patient-container">
      <h2>Add New Patient</h2>
      <form onSubmit={handleSubmit} className="add-patient-form">
        <div className="form-group">
          <label htmlFor="doj">Date of Joining:</label>
          <input
            type="date"
            id="doj"
            name="doj"
            value={formData.doj}
            disabled
          />
        </div>
        <div className="form-group">
          <label htmlFor="bloodGroup">Blood Group:</label>
          <select
            id="bloodGroup"
            name="bloodGroup"
            value={formData.bloodGroup}
            onChange={handleChange}
            required
          >
            <option value="">Select Blood Group</option>
            {bloodGroups.map((group) => (
              <option key={group} value={group}>
                {group}
              </option>
            ))}
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="allergies">Allergies:</label>
          <input
            type="text"
            id="allergies"
            name="allergies"
            value={formData.allergies}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label htmlFor="chronicDiseases">Chronic Diseases:</label>
          <input
            type="text"
            id="chronicDiseases"
            name="chronicDiseases"
            value={formData.chronicDiseases}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label htmlFor="emergencyContactName">Emergency Contact Name:</label>
          <input
            type="text"
            id="emergencyContactName"
            name="emergencyContactName"
            value={formData.emergencyContactName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="emergencyContactNumber">Emergency Contact Number:</label>
          <input
            type="text"
            id="emergencyContactNumber"
            name="emergencyContactNumber"
            value={formData.emergencyContactNumber}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="insuranceProvider">Insurance Provider:</label>
          <input
            type="text"
            id="insuranceProvider"
            name="insuranceProvider"
            value={formData.insuranceProvider}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label htmlFor="insuranceNumber">Insurance Number:</label>
          <input
            type="text"
            id="insuranceNumber"
            name="insuranceNumber"
            value={formData.insuranceNumber}
            onChange={handleChange}
          />
        </div>
        <div className="form-group full-width">
          <label htmlFor="medicalHistoryNotes">Medical History Notes:</label>
          <textarea
            id="medicalHistoryNotes"
            name="medicalHistoryNotes"
            value={formData.medicalHistoryNotes}
            onChange={handleChange}
            rows={5}
          ></textarea>
        </div>
        <button type="submit" className="submit-button">Add Patient</button>
      </form>
    </div>
  );
};

export default AddPatient;
