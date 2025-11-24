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
            // IF APPOINTMENT DETAILS IS NULL RETURN BAD REQUEST
            if(appointment == null)
            {
                return BadRequest(AppConstants.ResponseMessages.AppointmentDetailsRequired);
            }

            // CALL BUSINESS LAYER TO INSERT APPOINTMENT
            var result = await _appointmentBL.InsertAppointment(appointment);

            // IF INSERTION FAILED RETURN BAD REQUEST
            if(result != AppConstants.DBResponse.Success )
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.DoctorAppointmentFixed);
        }

        [HttpPut]
        [CustomAuth(Roles.Admin, Roles.Doctor)]
        public async Task<IActionResult> UpdateAppointment(Appointment appointment)
        {
            // IF APPOINTMENT DETAILS IS NULL RETURN BAD REQUEST
            if (appointment == null)
            {
                return BadRequest(AppConstants.ResponseMessages.AppointmentDetailsRequired);
            }

            // CALL BUSINESS LAYER TO UPDATE APPOINTMENT
            var result = await _appointmentBL.UpdateAppointment(appointment);

            // IF UPDATE FAILED RETURN BAD REQUEST
            if (result != AppConstants.DBResponse.Success)
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.DoctorAppointmentUpdates);

        }

        [HttpGet]
        [CustomAuth(Roles.Admin, Roles.Doctor, Roles.Patient, Roles.FrontDesk)]
        public async Task<IActionResult> GetAllAppointments()
        {
            // CALL BUSINESS LAYER TO GET ALL APPOINTMENTS
            var result = await _appointmentBL.GetAllAppointmentDetails();

            // IF NO APPOINTMENTS FOUND RETURN NOT FOUND
            if(result == null)
            {
                return NotFound(AppConstants.ResponseMessages.AppointmentsNotFound);
            }

            // RETURN APPOINTMENTS
            return Ok(result);
        }

        [HttpGet("{appointmentId}")]
        [CustomAuth(Roles.Admin, Roles.Doctor, Roles.Patient, Roles.FrontDesk)]
        public async Task<IActionResult> GetAppointment(int appointmentId)
        {
            // IF APPOINTMENT ID IS LESS THAN OR EQUAL TO ZERO RETURN BAD REQUEST
            if (appointmentId <= 0)
            {
                return BadRequest(AppConstants.ResponseMessages.AppointmentIdRequired);
            }

            // CALL BUSINESS LAYER TO GET APPOINTMENT DETAIL
            var result = await _appointmentBL.GetAppointmentDetail(appointmentId);

            // IF APPOINTMENT NOT FOUND RETURN NOT FOUND
            if(result == null)
            {
                return NotFound(AppConstants.ResponseMessages.AppointmentNotFound);
            }

            // RETURN APPOINTMENT DETAIL
            return Ok(result);
        }
    }
}
