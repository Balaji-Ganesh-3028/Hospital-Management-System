using BusinessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorBL _doctorBL;
        public DoctorController(IDoctorBL doctorBL)
        {
            _doctorBL = doctorBL;
        }

        [HttpGet("all-doctors")]
        public async Task<IActionResult> GetAllDoctor()
        {
            var result = await _doctorBL.GetAllDoctorDetails();
            return Ok(result);
        }

        [HttpGet("doctor")]
        public async Task<IActionResult> GetDoctor([FromQuery] int userId)
        {
            var result = await _doctorBL.GetDoctorDetails(userId);
            return Ok(result);
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertDoctorDetail(DoctorDetails doctorDetails)
        {
            if (doctorDetails == null)
            {
                return BadRequest("Doctor details required");
            }

            var result = await _doctorBL.InsertDoctorDetails(doctorDetails);

            if (result == "Success") return Ok("Details added successfully");
            else return BadRequest("Something went wrong!!!");
        }

    }
}
