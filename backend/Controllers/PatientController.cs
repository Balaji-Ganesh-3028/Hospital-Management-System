using AppModels.Models;
using backend.CustomAttributes;
using backend.Enum;
using BusinessLayer.Interface;
using Constant.Constants;
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
            // IF PATIENT DETAILS IS NULL RETURN BAD REQUEST
            if (patientDetails == null) 
            { 
                return BadRequest(AppConstants.ResponseMessages.PatientDetailsRequired);
            }

            // CALL BUSINESS LAYER TO INSERT PATIENT DETAILS
            var result = await _patientBL.InsertPatientDetails(patientDetails);

            // CHECK RESULT AND RETURN APPROPRIATE RESPONSE
            if (result != AppConstants.DBResponse.Success)
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.PatientDetailsAddedSuccessfully);
        }

        [HttpPut]
        [CustomAuth(Roles.Admin, Roles.Patient)]
        public async Task<IActionResult> UpdatePatientDetails([FromBody] PatientDetails patientDetails)
        {
            // IF PATIENT DETAILS IS NULL RETURN BAD REQUEST
            if (patientDetails == null)
            {
                return BadRequest(AppConstants.ResponseMessages.PatientDetailsRequired);
            }

            // CALL BUSINESS LAYER TO UPDATE PATIENT DETAILS
            var result = await _patientBL.UpdatePatientDetails(patientDetails);

            // IF RESPONSE IS NOT SUCCESS RETURN BAD REQUEST
            if (result != AppConstants.DBResponse.Success)
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.PatientDetailsUpdatedSuccessfully);
        }

        [HttpGet]
        [CustomAuth(Roles.Admin, Roles.FrontDesk, Roles.Doctor, Roles.Patient)]
        public async Task<IActionResult> GetAllPatient()
        {
            // CALL BUSINESS LAYER TO GET ALL PATIENT DETAILS
            var result = await _patientBL.GetAllPatientDetails();

            // IF NO PATIENT DETAILS FOUND RETURN NOT FOUND
            if (result == null)
            {
                return NotFound(AppConstants.ResponseMessages.PatientDetailsNotFound);
            }

            // RETURN PATIENT DETAILS
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [CustomAuth(Roles.Admin, Roles.FrontDesk, Roles.Doctor, Roles.Patient)]
        public async Task<IActionResult> GetPatientDetails(int userId)
        {
            // IF USER ID IS LESS THAN OR EQUAL TO ZERO RETURN BAD REQUEST
            if (userId <= 0)
            {
                return BadRequest(AppConstants.ResponseMessages.PatientIdRequired);
            }

            // CALL BUSINESS LAYER TO GET PATIENT DETAILS BY USER ID
            var result = await _patientBL.GetPatientDetails(userId);
            // IF NO PATIENT DETAILS FOUND RETURN NOT FOUND
            if (result == null)
            {
                return NotFound(AppConstants.ResponseMessages.PatientDetailNotFound);
            }

            // RETURN PATIENT DETAILS
            return Ok(result);
        }
    }
}
