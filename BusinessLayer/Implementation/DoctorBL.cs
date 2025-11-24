using AppModels.Models;
using BusinessLayer.Interface;
using DataAccessLayer.Interface;

namespace BusinessLayer.Implementation
{
    public class DoctorBL : IDoctorBL
    {
        private readonly IDoctorDAL _doctorDAL;

        public DoctorBL(IDoctorDAL doctorDAL)
        {
            _doctorDAL = doctorDAL;
        }
        public async Task<object> GetAllDoctorDetails()
        {
            // CALL DATA ACCESS LAYER TO GET ALL DOCTOR DETAILS
            return await _doctorDAL.GetAllDoctorDetails();
        }

        public async Task<object> GetDoctorDetails(int userId)
        {
            // CALL DATA ACCESS LAYER TO GET DOCTOR DETAILS BY USER ID
            return await _doctorDAL.GetDoctorDetails(userId);
        }

        public async Task<string> InsertDoctorDetails(DoctorDetails doctorDetails)
        {
            // CALL DATA ACCESS LAYER TO INSERT DOCTOR DETAILS
            return await _doctorDAL.InsertUpdateDoctorDetails(doctorDetails);
        }

        public async Task<string> UpdateDoctorDetails(DoctorDetails doctorDetails)
        {
            // CALL DATA ACCESS LAYER TO UPDATE DOCTOR DETAILS
            return await _doctorDAL.InsertUpdateDoctorDetails(doctorDetails);
        }
    }
}
