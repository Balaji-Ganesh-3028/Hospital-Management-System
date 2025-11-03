using DataAccessLayer.Models;

namespace DataAccessLayer.Interface
{
    public interface IPatientDAL
    {
        public Task<string> InsertUpdatePatientDetails(PatientDetails patientDetails);
        public Task<object> GetAllPatientDetails();
        public Task<object> GetPatientDetails(int userId);
    }
}
