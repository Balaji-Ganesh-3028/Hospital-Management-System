using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IAppointmentDAL
    {
        public Task<string> InsertUpdateAppointmrnt(Appointment appointment);
        public Task<List<AppointmentDetailsResponse>> GellAllAppointment();
        public Task<AppointmentDetailsResponse> GellAppointment(int appointmentId);
    }
}
