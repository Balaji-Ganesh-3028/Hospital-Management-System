import PatientDashboard from "./PatientDashboard";
import DoctorDashboard from "./DoctorDashboard";
import Dashboard from "./Dashboard-old";


const DashboardLayout = () => {
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  // const isFrontDesk = user?.roleName === 'Front Desk';
  // const isAdmin = user?.roleName === 'Admin';
  const isDoctor = user?.roleName === 'Doctor';
  const isPatient = user?.roleName === 'Patient';


  return (
    <div>
      {isPatient ? (
        <PatientDashboard />
      ) : isDoctor ? (
        <DoctorDashboard />
      ) : (
        <Dashboard/>
      )}
    </div>
  );
};

export default DashboardLayout;
