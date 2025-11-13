import React, { useEffect, useState } from 'react';
import './UserProfile.css';
import { toGetUserId, UserProfilePayload } from '../Helpers/User-Profile-Payload';
import type { UserProfileFormData } from '../../../Models/User-Profile';
import { GetUserProfile, InsertUserProfile, UpdateUserProfile } from '../../../Service/User-Profile/user-profile';
import type { Lookup } from '../../../Models/Lookups';
import { notify } from '../../../Service/Toast-Message/Toast-Message';
import { ToastMessageTypes } from '../../../Enums/Toast-Message';
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import { GetGender } from '../../../Service/Lookups/Lookups';
import { useSearchParams } from 'react-router-dom';

const UserProfile: React.FC = () => {
  const today = new Date().toISOString().split('T')[0];
  const [gender, setGender] = useState<Lookup[]>([]);
  const { showSpinner, hideSpinner } = useSpinner();
  const [searchParams] = useSearchParams();

  const user = JSON.parse(localStorage.getItem('user') || ''); // Use the useAuth hook
  const isFrontDesk = user?.roleName == 'Front Desk'; // Check if the user is Front Desk
  const isPatient = user?.roleName == 'Patient'; // Check if the user is Patient
  const isDoctor = user?.roleName == 'Doctor'; // Check if the user is Doctor
  const isAdmin = user?.roleName == 'Admin' || 'Super Admin'; // Check if the user is Admin & Super Admin

  const [userId, setUserId] = useState<number>(0);

  const [formData, setFormData] = useState<UserProfileFormData>({
    firstName: '',
    lastName: '',
    gender: '',
    age: '',
    dob: '',
    phoneNumber: '',
    doorFloorBuilding: '',
    addressLine1: '',
    addressLine2: '',
    country: '',
    city: '',
    state: '',
    pincode: '',
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { id, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [id]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Form Data Submitted:', formData);

    // UPDATE USER PROFILE
    if(searchParams.get('userId')) {
      const payload = await UserProfilePayload(formData, Number(searchParams.get('userId')));

      try {
        const updateResponse = await UpdateUserProfile(payload);

        if (updateResponse == "User details updated successfully") {
          notify(updateResponse, ToastMessageTypes.SUCCESS);
        }
        else notify("Somthing went wrong!!!", ToastMessageTypes.ERROR)
        return; 
      } catch(error) {
        console.error('Error updating user profile:', error);
        notify("Something went wrong!!!", ToastMessageTypes.ERROR);
      }
    }

    // INSERT USER PROFILE
    try {
      showSpinner();
      const payload = UserProfilePayload(formData, userId);

      // INSERT USER PROFILE
        const response = await InsertUserProfile(payload);
        if (response == "User details added successfully") {
          notify(response, ToastMessageTypes.SUCCESS);
          // if (isFrontDesk) navigate('/users?page=userList');
        }
        else notify("Somthing went wrong!!!", ToastMessageTypes.ERROR);
        return; 
    } catch (error) {
      console.error('Error inserting user profile:', error);
      notify("Something went wrong!!!", ToastMessageTypes.ERROR);
      hideSpinner();
    }
    
    setTimeout(() => {
      hideSpinner();
    }, 800)
  };

  useEffect(() => {
    loadLookupData(); // TO LOAD LOOKUP DATA
    console.log(searchParams.get('userId'), 'userId');
    if (searchParams.get('userId') && (isFrontDesk || isAdmin)) {
      console.log(searchParams.get('userId'));
      setUserId(parseInt(searchParams.get('userId') || '0'));
      
      loadUserDetails(Number(searchParams.get('userId') || '0'));
    }
    

    // IF PATIENT IS LOGIN THE NEED TO SHOW THE PATIENT DETAILS AND DOCTOR DETAILS
    if (isPatient || isDoctor) {
      const data = JSON.parse(localStorage.getItem('user') || '');
      if (data) setUserId(data?.userId);
      loadUserDetails(data?.userId);
    }
    
  }, []);

  const loadUserDetails = async (userId: number) => {
    const response = await GetUserProfile(Number(userId));
      console.log(response);
      setFormData({
        firstName: response.firstName,
        lastName: response.lastName,
        gender: response.gender,
        age: response.age,
        dob: response.dob.split('T')[0],
        phoneNumber: response.phoneNumber,
        doorFloorBuilding: response.doorFloorBuilding,
        addressLine1: response.addressLine1,
        addressLine2: response.addressLine2,
        country: response.country,
        city: response.city,
        state: response.state,
        pincode: response.pincode
      })
  }

  const loadLookupData = async () => {
    showSpinner();
    const gender = await GetGender();
    setGender(gender);
    setTimeout(() => {
      hideSpinner();
    }, 600)
  }

  return (
    <div className="user-profile-container">
      <form onSubmit={handleSubmit}>
        <div className="profile-sections">
          <div className="profile-section">
            <h2>User Profile</h2>
            <div className="form-grid">
              <div className="form-group">
                <label htmlFor="firstName">First Name</label>
                <input type="text" id="firstName" value={formData.firstName} onChange={handleChange} required />
              </div>
              <div className="form-group">
                <label htmlFor="lastName">Last Name</label>
                <input type="text" id="lastName" value={formData.lastName} onChange={handleChange} required />
              </div>
              <div className="form-group">
                <label htmlFor="gender">Gender</label>
                <select id="gender" value={formData.gender} onChange={handleChange} required>
                  <option value="">Select Gender</option>
                  {gender.map((gender) => (
                    <option key={gender.id} value={gender.id}>{ gender.name}</option>
                  ))}
                </select>
              </div>
              <div className="form-group">
                <label htmlFor="age">Age</label>
                <input type="number" id="age" value={formData.age} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label htmlFor="dob">Date of Birth</label>
                <input type="date" id="dob" max={today} value={formData.dob} onChange={handleChange} required />
              </div>
            </div>
          </div>
          <div className="profile-section">
            <h2>Contact Details</h2>
            <div className="form-grid">
              <div className="form-group">
                <label htmlFor="phoneNumber">Phone Number</label>
                <input type="tel" id="phoneNumber" value={formData.phoneNumber} onChange={handleChange} required />
              </div>
              <div className="form-group">
                <label htmlFor="doorFloorBuilding">Door/Floor/Building No</label>
                <input type="text" id="doorFloorBuilding" value={formData.doorFloorBuilding} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label htmlFor="addressLine1">Address Line 1</label>
                <input type="text" id="addressLine1" value={formData.addressLine1} onChange={handleChange} required />
              </div>
              <div className="form-group">
                <label htmlFor="addressLine2">Address Line 2</label>
                <input type="text" id="addressLine2" value={formData.addressLine2} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label htmlFor="country">Country</label>
                <input type="text" id="country" value={formData.country} onChange={handleChange} required />
              </div>
              <div className="form-group">
                <label htmlFor="city">City</label>
                <input type="text" id="city" value={formData.city} onChange={handleChange} required />
              </div>
              <div className="form-group">
                <label htmlFor="state">State</label>
                <input type="text" id="state" value={formData.state} onChange={handleChange} required />
              </div>
              <div className="form-group">
                <label htmlFor="pincode">Pincode</label>
                <input type="text" id="pincode" value={formData.pincode} onChange={handleChange} required />
              </div>
            </div>
          </div>
        </div>
        {/* <div className="profile-actions"> */}
          {/* <button type="submit">Save Changes</button> */}
          <button type="submit" className="submit-button">Submit</button>
        {/* </div> */}
      </form>
    {/* <AddPatient/> */}
    </div>

  );
};

export default UserProfile;
