import React, { useState, useEffect } from 'react';
import './DoctorList.css';
import { GetAllDoctors } from '../../../Service/Doctor/Doctor';
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import type { DoctorDetails } from '../../../Models/Doctor';
import { useNavigate } from 'react-router-dom';
interface DoctorListProps {
  onClickEdit: () => void;
}
function DoctorList({onClickEdit}: DoctorListProps) {
  const [doctors, setDoctors] = useState<DoctorDetails[]>([]);
  const { showSpinner, hideSpinner } = useSpinner();
  const  user = JSON.parse(localStorage.getItem('user') || ''); // Use the useAuth hook
  // const isFrontDesk = user?.roleName == 'Front Desk'; // Check if the user is Front Desk
  // const isPatient = user?.roleName == 'Patient'; // Check if the user is Patient
  const isAdmin = user?.roleName == 'Admin'; // Check if the user is Admin
  const navigate = useNavigate();

  useEffect(() => {
    const fetchDoctors = async () => {
      showSpinner();
      try {
        const response = await GetAllDoctors();
        if (response.data) {
          setDoctors(response.data);
        }
      } catch (error) {
        console.error("Error fetching doctors:", error);
      } finally {
        hideSpinner();
      }
    };

    fetchDoctors();
  }, []);

  const navigateToDoctorProfile = (id: number) => {
    onClickEdit();
    navigate(`/doctors?page=addDoctor&action=edit&userId=${id}`);
  };

  return (
    <div className="doctor-list-container">
      {/* <div className="doctor-list-actions">
        <button><i className="fas fa-plus"></i> Add Doctor</button>
      </div> */}
      <div className="table-responsive">
        <table className="app-table">
        <thead>
          <tr>
            <th>Doctor Id</th>
            <th>First Name</th>
            <th>Last Name</th>
            {/* <th>Date of Association</th> */}
            {/* <th>License Number</th> */}
            <th>Credential</th>
            <th>Specialization</th>
            <th>Designation</th>
            <th>Experience (Years)</th>
            {isAdmin && <th>Actions</th>}
          </tr>
        </thead>
        <tbody>
          {doctors.map((doctor) => (
            <tr key={doctor.id}>
              <td>{doctor.id}</td>
              <td>{doctor.firstName}</td>
              <td>{doctor.lastName}</td>
              {/* <td>{doctor.dateOfAssociation}</td> */}
              {/* <td>{doctor.licenseNumber}</td> */}
              <td>{doctor.qualificationName}</td>
              <td>{doctor.specialisationName}</td>
              <td>{doctor.designationName}</td>
              <td>{doctor.experienceYears}</td>
              {isAdmin && <td>
                <button className="edit-btn" onClick={() => {navigateToDoctorProfile(Number(doctor.id))}}><i className="fas fa-edit"></i></button>
                {/* <button className="delete-btn"><i className="fas fa-trash"></i></button> */}
              </td>}
            </tr>
          ))}
        </tbody>
        </table>
        </div>
    </div>
  );
};

export default DoctorList;
