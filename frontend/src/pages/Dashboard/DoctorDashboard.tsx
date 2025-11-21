import { useState, useEffect } from "react";
import { GetDoctorDetails } from "../../Service/Doctor/Doctor";
import { GetAllAppointments } from "../../Service/Appointment/Appointment";
import type { DoctorDetails } from "../../Models/Doctor";
import type { AppointmentDetails } from "../../Models/Appointment";
import "./DoctorDashboard.css";

const DoctorDashboard = () => {
  const [doctor, setDoctor] = useState<DoctorDetails | null>(null);
  const [todaysAppointments, setTodaysAppointments] = useState<AppointmentDetails[]>([]);
  const [upcomingAppointments, setUpcomingAppointments] = useState<AppointmentDetails[]>([]);
  const [pastAppointments, setPastAppointments] = useState<AppointmentDetails[]>([]);
  const [doctorPatients, setDoctorPatients] = useState<any[]>([]);
  const [stats, setStats] = useState({ totalPatients: 0, appointmentsToday: 0 });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const user = JSON.parse(localStorage.getItem('user') || '{}');

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const doctorResponse = await GetDoctorDetails(user?.userId);
        const doctorData = doctorResponse.data;
        setDoctor(doctorData);

        if (doctorData && doctorData) {
          const appointmentsResponse = await GetAllAppointments();
          const allAppointments = appointmentsResponse.data;

          const doctorAppointments = allAppointments.filter(
            (appt: AppointmentDetails) => appt.doctorInfo.doctorId === doctorData.doctorId
          );

          const now = new Date();
          const upcoming: AppointmentDetails[] = [];
          const past: AppointmentDetails[] = [];

          doctorAppointments.forEach((appt: AppointmentDetails) => {
            if (new Date(appt.appointmentDetails.appointmentDate) >= now) {
              upcoming.push(appt);
            } else {
              past.push(appt);
            }
          });

          const today = new Date().toISOString().split("T")[0];
          const todayAppts = doctorAppointments.filter(
            (appt: AppointmentDetails) => appt.appointmentDetails.appointmentDate.split("T")[0] === today
          );
          setTodaysAppointments(todayAppts);

          const mockUpcomingAppointments: AppointmentDetails[] = [
            { appointmentId: 101, appointmentDate: '2025-12-01', patientFirstName: 'Alice', patientLastName: 'Smith', status: 'Scheduled' },
            { appointmentId: 102, appointmentDate: '2025-12-05', patientFirstName: 'Bob', patientLastName: 'Johnson', status: 'Scheduled' },
            { appointmentId: 103, appointmentDate: '2025-12-10', patientFirstName: 'Charlie', patientLastName: 'Brown', status: 'Scheduled' },
            { appointmentId: 104, appointmentDate: '2025-12-15', patientFirstName: 'Diana', patientLastName: 'Prince', status: 'Scheduled' },
          ];

          const mockPastAppointments: AppointmentDetails[] = [
            { appointmentId: 201, appointmentDate: '2025-10-20', patientFirstName: 'Eve', patientLastName: 'Adams', status: 'Completed' },
            { appointmentId: 202, appointmentDate: '2025-10-15', patientFirstName: 'Frank', patientLastName: 'White', status: 'Completed' },
            { appointmentId: 203, appointmentDate: '2025-10-10', patientFirstName: 'Grace', patientLastName: 'Green', status: 'Completed' },
            { appointmentId: 204, appointmentDate: '2025-10-05', patientFirstName: 'Heidi', patientLastName: 'Black', status: 'Completed' },
          ];

          setUpcomingAppointments([...upcoming, ...mockUpcomingAppointments]);
          setPastAppointments([...past, ...mockPastAppointments]);

          const uniquePatients = Array.from(
            new Map(doctorAppointments.map((appt: { patientId: any; patientFirstName: any; patientLastName: any; }) => [appt.patientId, {
              id: appt.patientId,
              name: `${appt.patientFirstName} ${appt.patientLastName}`
            }])).values()
          );
          setDoctorPatients(uniquePatients);
          
          setStats({
            totalPatients: uniquePatients.length,
            appointmentsToday: todayAppts.length,
          });
        }

        // Mock data for Today's Schedule
        const mockTodaysAppointments: AppointmentDetails[] = [
          { appointmentId: 301, appointmentDate: "2025-11-12T20:00:00", patientFirstName: 'David', patientLastName: 'Lee', status: 'Scheduled' },
          { appointmentId: 302, appointmentDate: "2025-11-12T21:15:00", patientFirstName: 'Emily', patientLastName: 'Chen', status: 'Scheduled' },
        ];
        setTodaysAppointments(mockTodaysAppointments);

        // Mock data for Upcoming Appointments
        const mockUpcomingAppointments: AppointmentDetails[] = [
          { appointmentId: 101, appointmentDate: '2025-12-01', patientFirstName: 'Alice', patientLastName: 'Smith', status: 'Scheduled' },
          { appointmentId: 102, appointmentDate: '2025-12-05', patientFirstName: 'Bob', patientLastName: 'Johnson', status: 'Scheduled' },
          { appointmentId: 103, appointmentDate: '2025-12-10', patientFirstName: 'Charlie', patientLastName: 'Brown', status: 'Scheduled' },
          { appointmentId: 104, appointmentDate: '2025-12-15', patientFirstName: 'Diana', patientLastName: 'Prince', status: 'Scheduled' },
        ];
        setUpcomingAppointments(mockUpcomingAppointments);

        // Mock data for Past Appointments
        const mockPastAppointments: AppointmentDetails[] = [
          { appointmentId: 201, appointmentDate: '2025-10-20', patientFirstName: 'Eve', patientLastName: 'Adams', status: 'Completed' },
          { appointmentId: 202, appointmentDate: '2025-10-15', patientFirstName: 'Frank', patientLastName: 'White', status: 'Completed' },
          { appointmentId: 203, appointmentDate: '2025-10-10', patientFirstName: 'Grace', patientLastName: 'Green', status: 'Completed' },
          { appointmentId: 204, appointmentDate: '2025-10-05', patientFirstName: 'Heidi', patientLastName: 'Black', status: 'Completed' },
        ];
        setPastAppointments(mockPastAppointments);

        // Mock data for My Patients
        const mockDoctorPatients = [
          { id: 401, name: 'Anjali' },
          { id: 402, name: 'Vikram' },
          { id: 403, name: 'Meera' },
          { id: 404, name: 'Rahul' },
        ];
        setDoctorPatients(mockDoctorPatients);

        // Mock data for Card Section
        setStats(prev => ({
          totalPatients: prev.totalPatients + 10, // Adding to existing or setting a base
          appointmentsToday: prev.appointmentsToday + 5, // Adding to existing or setting a base
        }));

        setLoading(false);
      } catch (error) {
        setError("Failed to fetch doctor data.");
        setLoading(false);
        console.log("Error in docotr dashboard: ", error)
      }
    };

    fetchData();
  }, [user?.userId]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div className="doctor-dashboard">
      {doctor && <h2>Welcome, Dr. {doctor.firstName} {doctor.lastName}</h2>}

      {doctor && (
        <div className="dashboard-section no-header">
          <div className="stats-container">
            <div className="card text-card bg-success">
              <h4>Total Patients</h4>
              <p>{stats.totalPatients}</p>
            </div>
            <div className="card text-card bg-info">
              <h4>Appointments Today</h4>
              <p>{stats.appointmentsToday}</p>
            </div>
          </div>
        </div>
      )}

      <div className="dashboard-section">
        <h3>Today's Schedule</h3>
        {todaysAppointments.length > 0 ? (
          <ul>
            {todaysAppointments.map((appt) => (
              <li key={appt.appointmentDetails?.appointmentId}>
                <strong>{new Date(appt.appointmentDetails?.appointmentDate).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}</strong> - {appt.patientInfo?.patientFirstName} {appt.patientInfo?.patientLastName}
              </li>
            ))}
          </ul>
        ) : (
          <p>No appointments scheduled for today.</p>
        )}
      </div>

      <div className="dashboard-section">
        <h3>Upcoming Appointments</h3>
        {upcomingAppointments.length > 0 ? (
          <ul>
            {upcomingAppointments.map((appt) => (
              <li key={appt.appointmentDetails?.appointmentId}>
                <strong>{new Date(appt.appointmentDetails?.appointmentDate).toLocaleDateString()}</strong> with {appt.patientInfo?.patientFirstName} {appt.patientInfo?.patientLastName}
              </li>
            ))}
          </ul>
        ) : (
          <p>No upcoming appointments.</p>
        )}
      </div>

      <div className="dashboard-section">
        <h3>Appointment History</h3>
        {pastAppointments.length > 0 ? (
          <ul>
            {pastAppointments.map((appt) => (
              <li key={appt.appointmentDetails?.appointmentId}>
                <strong>{new Date(appt.appointmentDetails?.appointmentDate).toLocaleDateString()}</strong> with {appt.patientInfo?.patientFirstName} {appt.patientInfo?.patientLastName} - <i>{appt.appointmentDetails?.status}</i>
              </li>
            ))}
          </ul>
        ) : (
          <p>No past appointments.</p>
        )}
      </div>

      <div className="dashboard-section">
        <h3>My Patients</h3>
        {doctorPatients.length > 0 ? (
          <ul>
            {doctorPatients.map((patient) => (
              <li key={patient.id}>{patient.name}</li>
            ))}
          </ul>
        ) : (
          <p>No patients found.</p>
        )}
      </div>
    </div>
  );
};

export default DoctorDashboard;