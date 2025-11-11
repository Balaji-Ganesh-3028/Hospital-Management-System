using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
