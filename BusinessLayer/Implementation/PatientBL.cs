using AppModels.Models;
using BusinessLayer.Interface;
using DataAccessLayer.Interface;

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
            // CALL DATA ACCESS LAYER TO INSERT PATIENT DETAILS
            return await _patientDAL.InsertUpdatePatientDetails(patientDetails);
        }

        public async Task<string> UpdatePatientDetails(PatientDetails patientDetails)
        {
            // CALL DATA ACCESS LAYER TO UPDATE PATIENT DETAILS
            return await _patientDAL.InsertUpdatePatientDetails(patientDetails);
        }

        public async Task<object> GetAllPatientDetails()
        {
            // CALL DATA ACCESS LAYER TO GET ALL PATIENT DETAILS
            return await _patientDAL.GetAllPatientDetails();
        }

        public async Task<object> GetPatientDetails(int userId)
        {
            // CALL DATA ACCESS LAYER TO GET PATIENT DETAILS BY USER ID
            return await _patientDAL.GetPatientDetails(userId);
        }
    }
}
