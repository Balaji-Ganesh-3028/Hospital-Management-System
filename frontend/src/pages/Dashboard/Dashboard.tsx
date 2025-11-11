import React, { useEffect, useState } from 'react';
import './Dashboard.css';
import { Chart as ChartJS, ArcElement, Tooltip, Legend, type ChartData } from 'chart.js';
import { Pie } from 'react-chartjs-2';
import { GetAllAppointments } from '../../Service/Appointment/Appointment';
import type { AppointmentDetails } from '../../Models/Appointment';
import { GetAllDoctors } from '../../Service/Doctor/Doctor';
import type { DoctorDetails } from '../../Models/Doctor';
import { GetDashboard } from '../../Service/Dashboard/Dashboard';
import type { DashboardDetails } from '../../Models/Dashboard';

ChartJS.register(ArcElement, Tooltip, Legend);

const Dashboard: React.FC = () => {
  const [dashboardData, setDashboardData] = useState<DashboardDetails>();
  const [appointmentStatusData, setAppointmentStatusData] = useState<ChartData<'pie', number[], unknown> | null>(null);
  const [doctorStatusData, setDoctorStatusData] = useState<ChartData<'pie', number[], unknown> | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const appointmentResponse = await GetAllAppointments();
        const appointments: AppointmentDetails[] = appointmentResponse.data;

        const doctorResponse = await GetAllDoctors();
        const doctors: DoctorDetails[] = doctorResponse.data;

        const today = new Date().toISOString().split('T')[0];

        const todaysAppointments = appointments.filter(app => app.appointmentDate.split('T')[0] === today);

        const statusCounts = {
          completed: 0,
          scheduled: 0,
          cancelled: 0,
        };

        todaysAppointments.forEach(app => {
          if (app.status.toLowerCase() === 'completed') {
            statusCounts.completed++;
          } else if (app.status.toLowerCase() === 'scheduled') {
            statusCounts.scheduled++;
          } else if (app.status.toLowerCase() === 'cancelled') {
            statusCounts.cancelled++;
          }
        });

        const appointmentChartData: ChartData<'pie', number[], unknown> = {
          labels: ['Completed', 'Scheduled', 'Cancelled'],
          datasets: [
            {
              label: 'Appointments',
              data: [statusCounts.completed, statusCounts.scheduled, statusCounts.cancelled],
              backgroundColor: [
                'rgba(75, 192, 192, 0.6)',
                'rgba(54, 162, 235, 0.6)',
                'rgba(255, 99, 132, 0.6)',
              ],
              borderColor: [
                'rgba(75, 192, 192, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 99, 132, 1)',
              ],
              borderWidth: 1,
            },
          ],
        };

        setAppointmentStatusData(appointmentChartData);

        // Calculate doctor availability
        const availableDoctors = new Set(todaysAppointments.map(app => app.doctorFirstName));
        const availableCount = availableDoctors.size;
        const unavailableCount = doctors.length - availableCount;

        const doctorChartData: ChartData<'pie', number[], unknown> = {
          labels: ['Have appointment', 'Not have appointment'],
          datasets: [
            {
              label: 'Doctors',
              data: [availableCount, unavailableCount],
              backgroundColor: [
                'rgba(75, 192, 192, 0.6)',
                'rgba(255, 99, 132, 0.6)',
              ],
              borderColor: [
                'rgba(75, 192, 192, 1)',
                'rgba(255, 99, 132, 1)',
              ],
              borderWidth: 1,
            },
          ],
        };

        setDoctorStatusData(doctorChartData);

      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    loadDashboardDetails();
  }, [])

  const loadDashboardDetails = async () => {
    try {
      const response = await GetDashboard();
      if (response) {
        setDashboardData(response);
      }
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  }

  return (
    <div className="dashboard-container">
      <h1 className="dashboard-title">Dashboard</h1>
      <div className="dashboard-grid">
        <div className="card">
          <h2>Total Users</h2>
          <p>{dashboardData?.totalUserCount}</p>
        </div>
        <div className="card">
          <h2>Total Patients</h2>
          <p>{dashboardData?.patientCount}</p>
        </div>
        <div className="card">
          <h2>Total Doctors</h2>
          <p>{dashboardData?.doctorCount}</p>
        </div>
        <div className="card">
          <h2>Today's Appointments</h2>
          <p>{dashboardData?.totalAppointmentsAddedToday}</p>
        </div>
        <div className="card">
          <h2>Patients Added Today</h2>
          <p>{dashboardData?.patientAddedToday}</p>
        </div>
        <div className="card">
          <h2>Doctors Added Today</h2>
          <p>{dashboardData?.doctorAddedToday}</p>
        </div>
      </div>
      <div className="dashboard-cards">
        <div className="card appointments">
          <h2>Total Appointments</h2>
          <p>{dashboardData?.appointmentCount}</p>
        </div>
      </div>
      <div className="dashboard-charts">
        <div className="card chart-card">
          <h2>Today's Appointment Status</h2>
          {appointmentStatusData && <Pie data={appointmentStatusData} />}
        </div>
        <div className="card chart-card">
          <h2>Today's Doctor Status</h2>
          {doctorStatusData && <Pie data={doctorStatusData} />}
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
