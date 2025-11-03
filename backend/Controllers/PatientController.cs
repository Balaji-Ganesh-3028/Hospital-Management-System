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


        [HttpPost("insert")]
        public async Task<IActionResult> InsertPatientDetails([FromBody] PatientDetails patientDetails)
        {
            if (patientDetails == null) 
            { 
                return BadRequest("Patient details are required");
            }
            var result = await _patientBL.InsertPatientDetails(patientDetails);

            if (result == "Success")
            {
                return Ok("Success");
            } else
            {
                return BadRequest("Something went wrong!!!");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePatientDetails([FromBody] PatientDetails patientDetails)
        {
            var result = await _patientBL.UpdatePatientDetails(patientDetails);
            if (result == "Success")
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Something went wrong!!!");
            }
        }

        [HttpGet("get-all-patient")]
        public async Task<IActionResult> GetAllPatient()
        {
            var result = await _patientBL.GetAllPatientDetails();
            return Ok(result);
        }

        [HttpGet("get-patient")]
        public async Task<IActionResult> GetPatientDetails([FromQuery] int patientId)
        {
            var result = await _patientBL.GetPatientDetails(patientId);
            return Ok(result);
        }


    }
}
