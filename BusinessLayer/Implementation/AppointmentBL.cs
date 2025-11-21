using AppModels.Models;
using AppModels.ResponseModels;
using BusinessLayer.Interface;
using DataAccessLayer.Interface;

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

        public async Task<string> UpdateAppointment(Appointment appointment)
        {
            return await _appointment.InsertUpdateAppointmrnt(appointment);
        }
    }
}
