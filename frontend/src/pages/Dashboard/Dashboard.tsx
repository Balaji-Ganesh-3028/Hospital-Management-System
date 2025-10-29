import React from 'react';
import './Dashboard.css';

const Dashboard: React.FC = () => {
  return (
    <div className="dashboard-container">
      <h1 className="dashboard-title">Dashboard</h1>
      <div className="dashboard-grid">
        <div className="card">
          <h2>Total Patients</h2>
          <p>1,234</p>
        </div>
        <div className="card">
          <h2>Total Doctors</h2>
          <p>78</p>
        </div>
        <div className="card">
          <h2>Total Users</h2>
          <p>1500</p>
        </div>
        <div className="card">
          <h2>Patients Added Today</h2>
          <p>5</p>
        </div>
        <div className="card">
          <h2>Doctors Added Today</h2>
          <p>1</p>
        </div>
        <div className="card">
          <h2>Today's Appointments</h2>
          <p>12</p>
        </div>
      </div>
      <div className="dashboard-cards">
        <div className="card appointments">
          <h2>Total Appointments</h2>
          <p>56</p>
        </div>
      </div>
      <div className="dashboard-table">
        <h2>Recent Activity</h2>
        <table>
          <thead>
            <tr>
              <th>Date</th>
              <th>Action</th>
              <th>Details</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>2025-10-29</td>
              <td>New Patient Registered</td>
              <td>John Doe</td>
            </tr>
            <tr>
              <td>2025-10-29</td>
              <td>Appointment Scheduled</td>
              <td>Dr. Smith with Jane Doe</td>
            </tr>
            <tr>
              <td>2025-10-28</td>
              <td>Discharged Patient</td>
              <td>Peter Jones</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Dashboard;
