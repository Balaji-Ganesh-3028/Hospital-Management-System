import React, { useEffect, useState } from 'react';
import './AddPatient.css';
import { GetBloodGroup } from '../../../Service/Lookups/Lookups';
import type { Lookup } from '../../../Models/Lookups';
import { setAdaptor } from '../Helpers/set-adaptor';
import { GetPatient, InsertPatient } from '../../../Service/Patient/Patient';
import { notify } from '../../../Service/Toast-Message/Toast-Message';
import { ToastMessageTypes } from '../../../Enums/Toast-Message';
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import { useSearchParams } from 'react-router-dom';

const AddPatient: React.FC = () => {
  const today = new Date().toISOString().split('T')[0];
  const user = JSON.parse(localStorage.getItem('user') || ''); // Use the useAuth hook
  const isPatient = user?.roleName == 'Patient'; // Check if the user is Patient
  const isAdmin = user?.roleName == 'Admin'; // Check if the user is Patient

  const [searchParams] = useSearchParams(); 

  const { showSpinner, hideSpinner } = useSpinner();
  const [formData, setFormData] = useState({
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

  const [bloodGroups, setBloodGroups] = useState<Lookup[]>([]);

  const onload = async () => {
    const bloodGroups = await GetBloodGroup();
    setBloodGroups(bloodGroups);
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
    console.log('Patient Data:', formData);
    // Here you would typically send the data to your backend
    const payload = setAdaptor(formData);

    try {
      const response = await InsertPatient(payload);
      if (response) {
        notify(response, ToastMessageTypes.SUCCESS);
      }
    } catch (error) {
      console.error('Error inserting patient:', error);
      notify("Something went wrong!!!", ToastMessageTypes.ERROR)
    }
  };

  useEffect(() => {
    onload();
    if (isPatient || isAdmin) {
      toLoadPatientData()    
    }
  }, [])

  const toLoadPatientData = async () => {
    try {
      showSpinner();

      const isEditMode = searchParams.get("action");
      console.log('edit mode', isEditMode);
      if(isEditMode == 'edit') {
        const userId = searchParams.get("userId");
        const patientData = await GetPatient(Number(userId));
        console.log(patientData, 'patientData');
        patchValue(patientData);
        return;
      }

      const userDetails = JSON.parse(localStorage.getItem('user') || '');
      const userId = userDetails.userId;
      const patientData = await GetPatient(userId);
      console.log(patientData, 'patientData');
      patchValue(patientData);

      
    } catch (error) {
      console.error('Error fetching patient data:', error);
      hideSpinner();
    }
  };

  const patchValue = (patientData: any) => {
    if (patientData) {
        setFormData({
          doj: patientData?.data?.doj.split('T')[0],
          bloodGroup: patientData?.data?.bloodGroup,
          allergies: patientData?.data?.allergies,
          chronicDiseases: patientData?.data?.chronicDiseases,
          emergencyContactName: patientData?.data?.emergencyContactName,
          emergencyContactNumber: patientData?.data?.emergencyContactNumber,
          insuranceProvider: patientData?.data?.insuranceProvider,
          insuranceNumber: patientData?.data?.insuranceNumber,
          medicalHistoryNotes: patientData?.data?.medicalHistoryNotes
        })
        setTimeout(() => {
          hideSpinner();
        }, 600)
      }
  }

  return (
    <div className="add-patient-container">
      <h2>Patient Details</h2>
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
              <option key={group.id} value={group.id}>
                {group.name}
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
        <button type="submit" className="submit-button">Submit</button>
      </form>
    </div>
  );
};

export default AddPatient;
