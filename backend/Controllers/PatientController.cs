using BusinessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientBL _patientBL;
        public PatientController(IPatientBL patientBL) 
        {
            _patientBL = patientBL;
        }


        [HttpPost("")]
        public async Task<IActionResult> InsertPatientDetails([FromBody] PatientDetails patientDetails)
        {
            if (patientDetails == null) 
            { 
                return BadRequest("Patient details are required");
            }
            var result = await _patientBL.InsertPatientDetails(patientDetails);

            if (result == "Success")
            {
                return Ok("Patient details added successfully");
            } else
            {
                return BadRequest("Something went wrong!!!");
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdatePatientDetails([FromBody] PatientDetails patientDetails)
        {
            var result = await _patientBL.UpdatePatientDetails(patientDetails);
            if (result == "Success")
            {
                return Ok("Patient details updated successfully");
            }
            else
            {
                return BadRequest("Something went wrong!!!");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllPatient()
        {
            var result = await _patientBL.GetAllPatientDetails();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPatientDetails(int userId)
        {
            var result = await _patientBL.GetPatientDetails(userId);
            return Ok(result);
        }


    }
}
