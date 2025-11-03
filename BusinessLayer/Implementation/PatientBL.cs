using BusinessLayer.Interface;
using DataAccessLayer.Interface;
using DataAccessLayer.Models;

namespace BusinessLayer.Implementation
{
    public class PatientBL: IPatientBL
    {
        private readonly IPatientDAL _patientDAL;

        public PatientBL(IPatientDAL patientDAL)
        {
            _patientDAL = patientDAL;
        }

        public async Task<string> InsertPatientDetails(PatientDetails patientDetails)
        {
            return await _patientDAL.InsertUpdatePatientDetails(patientDetails);
        }

        public async Task<string> UpdatePatientDetails(PatientDetails patientDetails)
        {
            return await _patientDAL.InsertUpdatePatientDetails(patientDetails);
        }

        public async Task<object> GetAllPatientDetails()
        {
            return await _patientDAL.GetAllPatientDetails();
        }

        public async Task<object> GetPatientDetails(int patientId)
        {
            return await _patientDAL.GetPatientDetails(patientId);
        }
    }
}
