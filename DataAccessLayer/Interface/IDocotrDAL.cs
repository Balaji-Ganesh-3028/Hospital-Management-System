using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IDoctorDAL
    {
        public Task<string> InsertUpdateDoctorDetails(DoctorDetails doctorDetails);
        public Task<object> GetAllDoctorDetails();
        public Task<object> GetDoctorDetails(int userId);
    }
}
