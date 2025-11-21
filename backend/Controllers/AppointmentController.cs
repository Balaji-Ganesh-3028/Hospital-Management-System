using backend.CustomAttributes;
using backend.Enum;
using BusinessLayer.Interface;
using Constant.Constants;
using AppModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentBL _appointmentBL;

        public AppointmentController(IAppointmentBL appointmentBL)
        {
            _appointmentBL = appointmentBL;
        }

        [HttpPost]
        [CustomAuth(Roles.Admin, Roles.Doctor)]
        public async Task<IActionResult> InsertAppointment(Appointment appointment)
        {
            var result = await _appointmentBL.InsertAppointment(appointment);
            if(result == AppConstants.DBResponse.Success )
            {
                return Ok(AppConstants.ResponseMessages.DoctorAppointmentFixed);
            }
            else
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }
        }

        [HttpPut]
        [CustomAuth(Roles.Admin, Roles.Doctor)]
        public async Task<IActionResult> UpdateAppointment(Appointment appointment)
        {
            var result = await _appointmentBL.UpdateAppointment(appointment);
            if (result == AppConstants.DBResponse.Success)
            {
                return Ok(AppConstants.ResponseMessages.DoctorAppointmentUpdates);
            }
            else
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }
        }

        [HttpGet]
        [CustomAuth(Roles.Admin, Roles.Doctor, Roles.Patient, Roles.FrontDesk)]
        public async Task<IActionResult> GetAllAppointments()
        {
            var result = await _appointmentBL.GetAllAppointmentDetails();
            return Ok(result);
        }

        [HttpGet("{appointmentId}")]
        [CustomAuth(Roles.Admin, Roles.Doctor, Roles.Patient, Roles.FrontDesk)]
        public async Task<IActionResult> GetAppointment(int appointmentId)
        {
            var result = await _appointmentBL.GetAppointmentDetail(appointmentId);
            return Ok(result);
        }
    }
}
