import { useState, useEffect } from "react";
import { GetPatient } from "../../Service/Patient/Patient";
import { GetAllAppointments } from "../../Service/Appointment/Appointment";
import type { PatientDetails } from "../../Models/Patient";
import type { AppointmentDetails } from "../../Models/Appointment";
import "./PatientDashboard.css";

const PatientDashboard = () => {
  const [patient, setPatient] = useState<PatientDetails | null>(null);
  const [upcomingAppointments, setUpcomingAppointments] = useState<AppointmentDetails[]>([]);
  const [pastAppointments, setPastAppointments] = useState<AppointmentDetails[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const user = JSON.parse(localStorage.getItem('user') || '{}');

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const patientResponse = await GetPatient(user?.userId);
        const patientData = patientResponse.data;

        setPatient(patientData);

        if (patientData) {
          const appointmentsResponse = await GetAllAppointments();
          const allAppointments = appointmentsResponse.data;
          
          const patientAppointments = allAppointments.filter(
            (appt: AppointmentDetails) => appt.patientId === patientData.patientId
          );

          const now = new Date();
          const upcoming: AppointmentDetails[] = [];
          const past: AppointmentDetails[] = [];

          patientAppointments.forEach((appt: AppointmentDetails) => {
            if (new Date(appt.appointmentDate) >= now) {
              upcoming.push(appt);
            } else {
              past.push(appt);
            }
          });

          const mockUpcomingAppointments: AppointmentDetails[] = [
            { appointmentId: 101, appointmentDate: '2025-12-01', doctorFirstName: 'John', doctorLastName: 'Doe', status: 'Scheduled' },
            { appointmentId: 102, appointmentDate: '2025-12-05', doctorFirstName: 'Jane', doctorLastName: 'Smith', status: 'Scheduled' },
            { appointmentId: 103, appointmentDate: '2025-12-10', doctorFirstName: 'Peter', doctorLastName: 'Jones', status: 'Scheduled' },
            { appointmentId: 104, appointmentDate: '2025-12-15', doctorFirstName: 'Mary', doctorLastName: 'Williams', status: 'Scheduled' },
          ];

          const mockPastAppointments: AppointmentDetails[] = [
            { appointmentId: 201, appointmentDate: '2025-10-20', doctorFirstName: 'John', doctorLastName: 'Doe', status: 'Completed' },
            { appointmentId: 202, appointmentDate: '2025-10-15', doctorFirstName: 'Jane', doctorLastName: 'Smith', status: 'Completed' },
            { appointmentId: 203, appointmentDate: '2025-10-10', doctorFirstName: 'Peter', doctorLastName: 'Jones', status: 'Completed' },
            { appointmentId: 204, appointmentDate: '2025-10-05', doctorFirstName: 'Mary', doctorLastName: 'Williams', status: 'Completed' },
          ];

          setUpcomingAppointments([...upcoming, ...mockUpcomingAppointments]);
          setPastAppointments([...past, ...mockPastAppointments]);
        }
        setLoading(false);
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
      } catch (error) {
        setError("Failed to fetch patient data.");
        setLoading(false);
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
    <div className="patient-dashboard">
      {patient && <h2>Welcome, {patient.firstName} {patient.lastName}</h2>}

      {patient && (
        <div className="dashboard-section">
          <h3>Personal Information</h3>
          <div className="personal-info-grid">
            <p><strong>Name:</strong> {patient.firstName} {patient.lastName}</p>
            <p><strong>Email:</strong> {patient.email}</p>
            <p><strong>Age:</strong> {patient.age}</p>
            <p><strong>Blood Group:</strong> {patient.bloodGroupName}</p>
            <p><strong>Allergies:</strong> {patient.allergies}</p>
            <p><strong>Chronic Diseases:</strong> {patient.chronicDiseases}</p>
          </div>
        </div>
      )}

      <div className="dashboard-section">
        <h3>Upcoming Appointments</h3>
        {upcomingAppointments.length > 0 ? (
          <ul>
            {upcomingAppointments.map((appt) => (
              <li key={appt.appointmentId}>
                <strong>{new Date(appt.appointmentDate).toLocaleDateString()}</strong> with Dr. {appt.doctorFirstName} {appt.doctorLastName}
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
              <li key={appt.appointmentId}>
                <strong>{new Date(appt.appointmentDate).toLocaleDateString()}</strong> with Dr. {appt.doctorFirstName} {appt.doctorLastName} - <i>{appt.status}</i>
              </li>
            ))}
          </ul>
        ) : (
          <p>No past appointments.</p>
        )}
      </div>
    </div>
  );
};

export default PatientDashboard;