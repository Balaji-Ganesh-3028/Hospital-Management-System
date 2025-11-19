using backend.CustomAttributes;
using backend.Enum;
using BusinessLayer.Interface;
using Constant.Constants;
using DataAccessLayer.Models;
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


        [HttpPost]
        [CustomAuth(Roles.Admin, Roles.Patient)]
        public async Task<IActionResult> InsertPatientDetails([FromBody] PatientDetails patientDetails)
        {
            if (patientDetails == null) 
            { 
                return BadRequest(AppConstants.ResponseMessages.PatientDetailsRequired);
            }
            var result = await _patientBL.InsertPatientDetails(patientDetails);

            if (result == AppConstants.DBResponse.Success)
            {
                return Ok(AppConstants.ResponseMessages.PatientDetailsAddedSuccessfully);
            } else
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }
        }

        [HttpPut]
        [CustomAuth(Roles.Admin, Roles.Patient)]
        public async Task<IActionResult> UpdatePatientDetails([FromBody] PatientDetails patientDetails)
        {
            var result = await _patientBL.UpdatePatientDetails(patientDetails);
            if (result == AppConstants.DBResponse.Success)
            {
                return Ok(AppConstants.ResponseMessages.PatientDetailsUpdatedSuccessfully);
            }
            else
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }
        }

        [HttpGet]
        [CustomAuth(Roles.Admin, Roles.FrontDesk, Roles.Doctor, Roles.Patient)]
        public async Task<IActionResult> GetAllPatient()
        {
            var result = await _patientBL.GetAllPatientDetails();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [CustomAuth(Roles.Admin, Roles.FrontDesk, Roles.Doctor, Roles.Patient)]
        public async Task<IActionResult> GetPatientDetails(int userId)
        {
            var result = await _patientBL.GetPatientDetails(userId);
            return Ok(result);
        }
    }
}
