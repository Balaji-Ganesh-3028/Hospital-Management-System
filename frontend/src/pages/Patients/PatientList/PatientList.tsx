import React, { useState, useEffect } from 'react';
import './PatientList.css';
import { GetAllPatients } from '../../../Service/Patient/Patient';
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import type { PatientDetails } from '../../../Models/Patient';
import { useNavigate } from 'react-router-dom';
interface PatientListProps {
  onClickEdit: () => void;
}
function PatientList({ onClickEdit }: PatientListProps) {
  const [patients, setPatients] = useState<PatientDetails[]>([]);
  const { showSpinner, hideSpinner } = useSpinner();
  const  user = JSON.parse(localStorage.getItem('user') || ''); // Use the useAuth hook
  const isFrontDesk = user?.roleName == 'Front Desk'; // Check if the user is Front Desk
  const isPatient = user?.roleName == 'Patient'; // Check if the user is Patient
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPatients = async () => {
      showSpinner();
      try {
        const response = await GetAllPatients();
        if (response) {
          setPatients(response);
        }
      } catch (error) {
        console.error("Error fetching patients:", error);
        hideSpinner();
      } finally {
        hideSpinner();
      }
    };

    fetchPatients();
  }, []);

  const navigateToPatientProfile = (id: number) => {
    onClickEdit();
    navigate(`/patients?page=addPatient&action=edit&userId=${id}`);
  };

  return (
    <div className="patient-list-container">
      {/* <div className="patient-list-actions">
        <button onClick={() => navigate}><i className="fas fa-plus"></i> Add Patient</button>
      </div> */}
      <div className="table-responsive">
        <table className="app-table">
        <thead>
          <tr>
            <th>Patient Id</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Phone No.</th>
            <th>Blood Group</th>
            {(!isFrontDesk && !isPatient) && (<th>Actions</th>)}
          </tr>
        </thead>
        <tbody>
          {patients.map((patient) => (
            <tr key={patient.id}>
              <td>{patient.id}</td>
              <td>{patient.firstName}</td>
              <td>{patient.lastName}</td>
              <td>{patient.phoneNumber}</td>
              <td>{patient.bloodGroupName}</td>
              {(!isFrontDesk && !isPatient) && (<td>
                <button className="edit-btn" onClick={() => {navigateToPatientProfile(Number(patient.id))}}><i className="fas fa-edit"></i></button>
                {/* <button className="delete-btn"><i className="fas fa-trash"></i></button> */}
              </td>)}
            </tr>
          ))}
        </tbody>
        </table>
      </div>
    </div>
  );
};

export default PatientList;