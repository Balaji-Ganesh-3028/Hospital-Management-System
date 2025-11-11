using DataAccessLayer.Models;

namespace BusinessLayer.Interface
{
    public interface IPatientBL
    {
        public Task<string> InsertPatientDetails(PatientDetails patientDetails);
        public Task<string> UpdatePatientDetails(PatientDetails patientDetails);
        public Task<object> GetAllPatientDetails();
        public Task<object> GetPatientDetails(int userId);

    }
}
