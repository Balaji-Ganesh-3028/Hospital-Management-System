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

        [HttpGet("")]
        public async Task<IActionResult> GetAllDoctor()
        {
            var result = await _doctorBL.GetAllDoctorDetails();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetDoctor(int userId)
        {
            var result = await _doctorBL.GetDoctorDetails(userId);
            return Ok(result);
        }

        [HttpPost("")]
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

        [HttpPut("")]
        public async Task<IActionResult> UpdateDoctorDetail(DoctorDetails doctorDetails)
        {
            if (doctorDetails == null)
            {
                return BadRequest("Doctor details required");
            }

            var result = await _doctorBL.UpdateDoctorDetails(doctorDetails);

            if (result == "Success") return Ok("Details updated successfully");
            else return BadRequest("Something went wrong!!!");
        }

    }
}
