import React from 'react';
import './UserProfile.css';

const UserProfile: React.FC = () => {
  const today = new Date().toISOString().split('T')[0];

  return (
    <div className="user-profile-container">
      <h1>User Profile</h1>
      <form>
        <div className="profile-sections">
          <div className="profile-section">
            <h2>User Profile</h2>
            <div className="form-grid">
              <div className="form-group">
                <label htmlFor="firstName">First Name</label>
                <input type="text" id="firstName" required />
              </div>
              <div className="form-group">
                <label htmlFor="lastName">Last Name</label>
                <input type="text" id="lastName" required />
              </div>
              <div className="form-group">
                <label htmlFor="gender">Gender</label>
                <select id="gender" required>
                  <option value="">Select Gender</option>
                  <option value="male">Male</option>
                  <option value="female">Female</option>
                  <option value="other">Other</option>
                </select>
              </div>
              <div className="form-group">
                <label htmlFor="age">Age</label>
                <input type="number" id="age" />
              </div>
              <div className="form-group">
                <label htmlFor="dob">Date of Birth</label>
                <input type="date" id="dob" max={today} required />
              </div>
            </div>
          </div>
          <div className="profile-section">
            <h2>Contact Details</h2>
            <div className="form-grid">
              <div className="form-group">
                <label htmlFor="phoneNumber">Phone Number</label>
                <input type="tel" id="phoneNumber" required />
              </div>
              <div className="form-group">
                <label htmlFor="doorNo">Door/Floor/Building No</label>
                <input type="text" id="doorNo" />
              </div>
              <div className="form-group">
                <label htmlFor="address1">Address Line 1</label>
                <input type="text" id="address1" required />
              </div>
              <div className="form-group">
                <label htmlFor="address2">Address Line 2</label>
                <input type="text" id="address2" />
              </div>
              <div className="form-group">
                <label htmlFor="country">Country</label>
                <input type="text" id="country" required />
              </div>
              <div className="form-group">
                <label htmlFor="city">City</label>
                <input type="text" id="city" required />
              </div>
              <div className="form-group">
                <label htmlFor="state">State</label>
                <input type="text" id="state" required />
              </div>
              <div className="form-group">
                <label htmlFor="pincode">Pincode</label>
                <input type="text" id="pincode" required />
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
