import React from 'react';
import './DoctorList.css';

const DoctorList: React.FC = () => {
  const doctors = [
    {
      id: 1,
      firstName: 'Dr. Emily',
      lastName: 'White',
      dateOfAssociation: '2018-03-10',
      licenseNumber: 'LIC-12345',
      credential: 'MD',
      specialization: 'Cardiology',
      designation: 'Senior Cardiologist',
      experienceYear: 12,
    },
    {
      id: 2,
      firstName: 'Dr. David',
      lastName: 'Green',
      dateOfAssociation: '2020-07-22',
      licenseNumber: 'LIC-67890',
      credential: 'DO',
      specialization: 'Pediatrics',
      designation: 'Pediatrician',
      experienceYear: 5,
    },
  ];

  return (
    <div className="doctor-list-container">
      <div className="doctor-list-actions">
        <button><i className="fas fa-plus"></i> Add Doctor</button>
      </div>
      <table className="app-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Date of Association</th>
            <th>License Number</th>
            <th>Credential</th>
            <th>Specialization</th>
            <th>Designation</th>
            <th>Experience (Years)</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {doctors.map((doctor) => (
            <tr key={doctor.id}>
              <td>{doctor.id}</td>
              <td>{doctor.firstName}</td>
              <td>{doctor.lastName}</td>
              <td>{doctor.dateOfAssociation}</td>
              <td>{doctor.licenseNumber}</td>
              <td>{doctor.credential}</td>
              <td>{doctor.specialization}</td>
              <td>{doctor.designation}</td>
              <td>{doctor.experienceYear}</td>
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

export default DoctorList;
