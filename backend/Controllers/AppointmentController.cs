using BusinessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("")]
        public async Task<IActionResult> InsertAppointment(Appointment appointment)
        {
            var result = await _appointmentBL.InsertAppointment(appointment);
            if(result == "Success")
            {
                return Ok("Doctor Apponitment fixed");
            }
            else
            {
                return BadRequest("Something went worng!!!");
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateAppointment(Appointment appointment)
        {
            var result = await _appointmentBL.UpdateAppointment(appointment);
            if (result == "Success")
            {
                return Ok("Doctor Apponitment updated");
            }
            else
            {
                return BadRequest("Something went worng!!!");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var result = await _appointmentBL.GetAllAppointmentDetails();
            return Ok(result);
        }

        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetAppointment(int appointmentId)
        {
            var result = await _appointmentBL.GetAppointmentDetail(appointmentId);
            return Ok(result);
        }
    }
}
