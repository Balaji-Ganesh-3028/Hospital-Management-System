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

        [HttpPost("insert")]
        public async Task<IActionResult> InsertAppointment(Appointment appointment)
        {
            var result = await _appointmentBL.InsertAppointment(appointment);
            if(result == "Success")
            {
                return Ok("Doctor Apponitment fixed");
            } else
            {
                return BadRequest("Something went worng!!!");
            }
        }

        [HttpGet("all-appointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var result = await _appointmentBL.GetAllAppointmentDetails();
            return Ok(result);
        }

        [HttpGet("get-appointment")]
        public async Task<IActionResult> GetAppointment([FromQuery] int appointmentId)
        {
            var result = await _appointmentBL.GetAppointmentDetail(appointmentId);
            return Ok(result);
        }
    }
}
