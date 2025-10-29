import React, { useState } from 'react';
import './AddDoctor.css';

const AddDoctor: React.FC = () => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    dateOfAssociation: '',
    licenseNumber: '',
    credential: '',
    specialization: '',
    designation: '',
    experienceYear: '',
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
    console.log('Doctor Data:', formData);
    // Here you would typically send the data to your backend
    alert('Doctor added (check console for data)');
  };

  return (
    <div className="add-doctor-container">
      <h2>Add New Doctor</h2>
      <form onSubmit={handleSubmit} className="add-doctor-form">
        <div className="form-group">
          <label htmlFor="firstName">First Name:</label>
          <input
            type="text"
            id="firstName"
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="lastName">Last Name:</label>
          <input
            type="text"
            id="lastName"
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="dateOfAssociation">Date of Association:</label>
          <input
            type="date"
            id="dateOfAssociation"
            name="dateOfAssociation"
            value={formData.dateOfAssociation}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="licenseNumber">License Number:</label>
          <input
            type="text"
            id="licenseNumber"
            name="licenseNumber"
            value={formData.licenseNumber}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="credential">Credential:</label>
          <input
            type="text"
            id="credential"
            name="credential"
            value={formData.credential}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label htmlFor="specialization">Specialization:</label>
          <input
            type="text"
            id="specialization"
            name="specialization"
            value={formData.specialization}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="designation">Designation:</label>
          <input
            type="text"
            id="designation"
            name="designation"
            value={formData.designation}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label htmlFor="experienceYear">Experience (Years):</label>
          <input
            type="number"
            id="experienceYear"
            name="experienceYear"
            value={formData.experienceYear}
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit" className="submit-button">Add Doctor</button>
      </form>
    </div>
  );
};

export default AddDoctor;
