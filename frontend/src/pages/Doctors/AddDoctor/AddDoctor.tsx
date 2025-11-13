import React, { useEffect, useState } from 'react';
import './AddDoctor.css';
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import { GetDoctorDetails, InsertDoctor, UpdateDoctor } from '../../../Service/Doctor/Doctor';
import { GetDesignation, GetQualification, GetSpecialisation } from '../../../Service/Lookups/Lookups';
import type { Lookup } from '../../../Models/Lookups';
import { setAdaptor, toGetUserId } from '../Helpers/set-adaptor';
import { ToastMessageTypes } from '../../../Enums/Toast-Message';
import { notify } from '../../../Service/Toast-Message/Toast-Message';
import { useSearchParams } from 'react-router-dom';

const AddDoctor: React.FC = () => {
  const today = new Date().toISOString().split('T')[0];
  const {showSpinner, hideSpinner} = useSpinner();
  const  user = JSON.parse(localStorage.getItem('user') || ''); // Use the useAuth hook
  const isDoctor = user?.roleName == 'Doctor'; // Check if the user is Doctor
  const isAdmin = user?.roleName == 'Admin'; // Check if the user is Admin
  const [searchParams] = useSearchParams();

  const [formData, setFormData] = useState({
    dateOfAssociation: today,
    licenseNumber: '',
    qualification: 0,
    specialisation: 0,
    designation: 0,
    experienceYears: 0,
  });

  const [specialisations, setSpecialisations] = useState<Lookup[]>([]);
  const [qualifications, setQualifications] = useState<Lookup[]>([]);
  const [designations, setDesignations] = useState<Lookup[]>([]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Doctor Data:', formData);

    if (searchParams.get('userId')) {
      const payload = await setAdaptor(formData, Number(searchParams.get('userId')));
      try {
        const response = await UpdateDoctor(payload);
        if (response) {
          notify(response.data, ToastMessageTypes.SUCCESS);
        }
      } catch (error) {
        console.error('Error inserting doctor:', error);
        notify("Something went wrong!!!", ToastMessageTypes.ERROR)
      }
      return;
    }

    const payload = await setAdaptor(formData, toGetUserId());
    try {
      const response = await InsertDoctor(payload);
      if (response) {
        notify("Doctor details added successfully", ToastMessageTypes.SUCCESS);
      }
    } catch (error) {
      console.error('Error inserting doctor:', error);
      notify("Something went wrong!!!", ToastMessageTypes.ERROR)
    }
  };

  useEffect(() => {
    const fetchLookups = async () => {
      try {
        showSpinner();
        const [specData, qualData, desigData] = await Promise.all([
          GetSpecialisation(),
          GetQualification(),
          GetDesignation(),
        ]);
        setSpecialisations(specData);
        setQualifications(qualData);
        setDesignations(desigData);

        setTimeout(() => {
          hideSpinner();
        }, 600);
      } catch (error) {
        console.error("Error fetching lookups:", error);
        hideSpinner();
      }
    };

    fetchLookups();
     if (isDoctor || isAdmin) {
      toLoadDoctorData()    
    }
  }, []);

  const toLoadDoctorData = async () => {
    try {
      showSpinner();

      const isEditMode = searchParams.get("action");
      if (isEditMode == 'edit') {
          const userId = searchParams.get("userId");
          const DoctorData = await GetDoctorDetails(Number(userId));
          console.log(DoctorData, 'patientData');
          patchValue(DoctorData);
          return;
      }
      
      const userDetails = JSON.parse(localStorage.getItem('user') || '');
      const doctorId = userDetails.userId;
      const response = await GetDoctorDetails(doctorId)
      console.log(response, 'response');
      patchValue(response);

      setTimeout(() => {
        hideSpinner()
      }, 600)
    } catch (error) {
     console.error('Error fetching patient data:', error); 
    }
  }

  const patchValue = (doctorData: any) => {
    if (doctorData?.data) {
      setFormData({
        dateOfAssociation: doctorData?.data?.dateOfAssociation.split('T')[0],
        licenseNumber: doctorData?.data?.licenseNumber,
        qualification: Number(doctorData?.data?.qualification),
        specialisation: Number(doctorData?.data?.specialisation),
        designation: doctorData?.data?.designation,
        experienceYears: doctorData?.data?.experienceYears
      })
    }
  }

  return (
    <div className="add-doctor-container">
      <h2>Doctor Details</h2>
      <form onSubmit={handleSubmit} className="add-doctor-form">
        <div className="form-group">
          <label htmlFor="dateOfAssociation">Date of Association:</label>
          <input
            type="date"
            id="dateOfAssociation"
            name="dateOfAssociation"
            value={formData.dateOfAssociation}
            onChange={handleChange}
            required disabled
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
          <label htmlFor="qualification">Credential:</label>
          <select
            id="qualification"
            name="qualification"
            value={formData.qualification}
            onChange={handleChange}
          >
            <option value="">Select Credential</option>
            {qualifications.map((qual) => (
              <option key={qual.id} value={qual.id}>
                {qual.name}
              </option>
            ))}
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="specialisation">Specialization:</label>
          <select
            id="specialisation"
            name="specialisation"
            value={formData.specialisation}
            onChange={handleChange}
            required
          >
            <option value="">Select Specialization</option>
            {specialisations.map((spec) => (
              <option key={spec.id} value={spec.id}>
                {spec.name}
              </option>
            ))}
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="designation">Designation:</label>
          <select
            id="designation"
            name="designation"
            value={formData.designation}
            onChange={handleChange}
          >
            <option value="">Select Designation</option>
            {designations.map((desig) => (
              <option key={desig.id} value={desig.id}>
                {desig.name}
              </option>
            ))}
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="experienceYears">Experience (Years):</label>
          <input
            type="number"
            id="experienceYears"
            name="experienceYears"
            value={formData.experienceYears}
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit" className="submit-button">Submit</button>
      </form>
    </div>
  );
};

export default AddDoctor;
