using AppModels.Models;

namespace DataAccessLayer.Interface
{
    public interface IDoctorDAL
    {
        public Task<string> InsertUpdateDoctorDetails(DoctorDetails doctorDetails);
        public Task<object> GetAllDoctorDetails();
        public Task<object> GetDoctorDetails(int userId);
    }
}
