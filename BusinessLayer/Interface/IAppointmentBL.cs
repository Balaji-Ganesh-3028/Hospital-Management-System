using AppModels.Models;
using AppModels.ResponseModels;

namespace BusinessLayer.Interface
{
    public interface IAppointmentBL
    {
        public Task<string> InsertAppointment(Appointment appointment);
        public Task<string> UpdateAppointment(Appointment appointment);
        public Task<List<AppointmentDetailsResponse>> GetAllAppointmentDetails();
        public Task<AppointmentDetailsResponse> GetAppointmentDetail(int appointmentId);
    }
}
