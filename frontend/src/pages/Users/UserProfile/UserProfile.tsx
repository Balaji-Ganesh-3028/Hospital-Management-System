import React, { useEffect, useState } from 'react';
import './UserProfile.css';
import { UserProfilePayload } from '../Helpers/User-Profile-Payload';
import type { UserProfileFormData } from '../../../Models/User-Profile';
import { GetGender, GetUserProfile, InsertUserProfile } from '../../../Service/User-Profile/user-profile';
import type { Lookup } from '../../../Models/Lookups';
import { notify } from '../../../Service/Toast-Message/Toast-Message';
import { ToastMessageTypes } from '../../../Enums/Toast-Message';
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import { useSearchParams } from 'react-router-dom';

const UserProfile: React.FC = () => {
  const today = new Date().toISOString().split('T')[0];
  const [gender, setGender] = useState<Lookup[]>([]);
  const { showSpinner, hideSpinner } = useSpinner();
  const [searchParams] = useSearchParams();

  const userId =searchParams.get('userId');

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
    
    showSpinner();
    const payload = UserProfilePayload(formData)
    const response = await InsertUserProfile(payload);
    if (response == "User details added successfully") notify(response, ToastMessageTypes.SUCCESS);
    else notify("Somthing went wrong!!!", ToastMessageTypes.ERROR)

    setTimeout(() => {
      hideSpinner();
    }, 800)
  };

  useEffect(() => {
    loadLookupData();
  }, []);

  const loadLookupData = async () => {
    showSpinner();
    const gender = await GetGender();
    setGender(gender);

    if (userId) {
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
    setTimeout(() => {
      hideSpinner();
    }, 600)
  }

  return (
    <div className="user-profile-container">
      <h1>User Profile</h1>
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
        <div className="profile-actions">
          <button type="submit">Save Changes</button>
        </div>
      </form>
    </div>
  );
};

export default UserProfile;
