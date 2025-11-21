using AppModels.Models;

namespace BusinessLayer.Interface
{
    public interface IDoctorBL
    {
        public Task<string> InsertDoctorDetails(DoctorDetails doctorDetails);
        public Task<string> UpdateDoctorDetails(DoctorDetails doctorDetails);
        public Task<object> GetAllDoctorDetails();
        public Task<object> GetDoctorDetails(int userId);
    }
}
