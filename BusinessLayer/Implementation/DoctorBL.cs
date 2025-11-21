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
            return await _doctorDAL.GetAllDoctorDetails();
        }

        public async Task<object> GetDoctorDetails(int userId)
        {
            return await _doctorDAL.GetDoctorDetails(userId);
        }

        public async Task<string> InsertDoctorDetails(DoctorDetails doctorDetails)
        {
            return await _doctorDAL.InsertUpdateDoctorDetails(doctorDetails);
        }

        public async Task<string> UpdateDoctorDetails(DoctorDetails doctorDetails)
        {
            return await _doctorDAL.InsertUpdateDoctorDetails(doctorDetails);
        }
    }
}
