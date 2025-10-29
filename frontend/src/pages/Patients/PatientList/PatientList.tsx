import React from 'react';
import './PatientList.css';

const PatientList: React.FC = () => {
  const patients = [
    {
      id: 1,
      firstName: 'Alice',
      lastName: 'Smith',
      doj: '2023-01-15',
      bloodGroup: 'A+',
      allergies: 'Penicillin',
      chronicDiseases: 'Asthma',
      emergencyContactName: 'Bob Smith',
      emergencyContactNumber: '123-456-7890',
      insuranceProvider: 'Blue Cross',
      insuranceNumber: 'INS-001',
      medicalHistoryNotes: 'Patient has a history of seasonal allergies.',
    },
    {
      id: 2,
      firstName: 'Charlie',
      lastName: 'Brown',
      doj: '2022-11-01',
      bloodGroup: 'O-',
      allergies: 'None',
      chronicDiseases: 'None',
      emergencyContactName: 'Sally Brown',
      emergencyContactNumber: '098-765-4321',
      insuranceProvider: 'Aetna',
      insuranceNumber: 'AET-002',
      medicalHistoryNotes: 'Regular check-ups, no major issues.',
    },
  ];

  return (
    <div className="patient-list-container">
      <div className="patient-list-actions">
        <button><i className="fas fa-plus"></i> Add Patient</button>
      </div>
      <table className="app-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>DOJ</th>
            <th>Blood Group</th>
            <th>Allergies</th>
            <th>Chronic Diseases</th>
            <th>Emergency Contact Name</th>
            <th>Emergency Contact Number</th>
            <th>Insurance Provider</th>
            <th>Insurance Number</th>
            <th>Medical History Notes</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {patients.map((patient) => (
            <tr key={patient.id}>
              <td>{patient.id}</td>
              <td>{patient.firstName}</td>
              <td>{patient.lastName}</td>
              <td>{patient.doj}</td>
              <td>{patient.bloodGroup}</td>
              <td>{patient.allergies}</td>
              <td>{patient.chronicDiseases}</td>
              <td>{patient.emergencyContactName}</td>
              <td>{patient.emergencyContactNumber}</td>
              <td>{patient.insuranceProvider}</td>
              <td>{patient.insuranceNumber}</td>
              <td>{patient.medicalHistoryNotes}</td>
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

export default PatientList;
