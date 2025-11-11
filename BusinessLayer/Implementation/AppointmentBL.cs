using BusinessLayer.Interface;
using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class AppointmentBL: IAppointmentBL
    {
        private readonly IAppointmentDAL _appointment;
        public AppointmentBL(IAppointmentDAL appointmentDAL) {
            _appointment = appointmentDAL;
        }

        public async Task<List<AppointmentDetailsResponse>> GetAllAppointmentDetails()
        {
            return await _appointment.GellAllAppointment();
        }

        public async Task<AppointmentDetailsResponse> GetAppointmentDetail(int appointmentId)
        {
            return await _appointment.GellAppointment(appointmentId);
        }

        public async Task<string> InsertAppointment(Appointment appointment)
        {
            return await _appointment.InsertUpdateAppointmrnt(appointment);
        }
    }
}
