using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAppointmentBL
    {
        public Task<string> InsertAppointment(Appointment appointment);
        public Task<List<AppointmentDetailsResponse>> GetAllAppointmentDetails();
        public Task<AppointmentDetailsResponse> GetAppointmentDetail(int appointmentId);
    }
}
