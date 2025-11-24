using AppModels.Models;
using AppModels.ResponseModels;
namespace DataAccessLayer.Interface
{
    public interface IAppointmentDAL
    {
        public Task<string> InsertUpdateAppointmrnt(Appointment appointment);
        public Task<List<AppointmentDetailsResponse>> GellAllAppointment();
        public Task<AppointmentDetailsResponse> GellAppointment(int appointmentId);
    }
}
