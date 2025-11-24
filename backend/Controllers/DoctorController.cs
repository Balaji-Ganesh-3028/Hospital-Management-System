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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorBL _doctorBL;
        public DoctorController(IDoctorBL doctorBL)
        {
            _doctorBL = doctorBL;
        }

        [HttpGet]
        [CustomAuth(Roles.Admin, Roles.FrontDesk, Roles.Doctor, Roles.Patient)]
        public async Task<IActionResult> GetAllDoctor()
        {
            // CALL BUSINESS LAYER TO GET ALL DOCTOR DETAILS
            var result = await _doctorBL.GetAllDoctorDetails();

            // IF NO DOCTOR DETAILS FOUND RETURN NOT FOUND
            if(result == null)
            {
                return NotFound(AppConstants.ResponseMessages.DoctorDetailsNotFound);
            }

            // RETURN DOCTOR DETAILS
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [CustomAuth(Roles.Admin, Roles.FrontDesk, Roles.Doctor, Roles.Patient)]
        public async Task<IActionResult> GetDoctor(int userId)
        {
            // IF USER ID IS LESS THAN OR EQUAL TO ZERO RETURN BAD REQUEST
            if(userId <= 0)
            {
                return BadRequest(AppConstants.ResponseMessages.DoctorIdRequired);
            }

            // CALL BUSINESS LAYER TO GET DOCTOR DETAILS BY USER ID
            var result = await _doctorBL.GetDoctorDetails(userId);

            // IF NO DOCTOR DETAILS FOUND RETURN NOT FOUND
            if (result == null)
            {
                return NotFound(AppConstants.ResponseMessages.DoctorDetailNotFound);
            }

            // RETURN DOCTOR DETAILS
            return Ok(result);
        }

        [HttpPost]
        [CustomAuth(Roles.Admin, Roles.Doctor)]
        public async Task<IActionResult> InsertDoctorDetail(DoctorDetails doctorDetails)
        {
            // IF DOCTOR DETAILS IS NULL RETURN BAD REQUEST
            if (doctorDetails == null)
            {
                return BadRequest(AppConstants.ResponseMessages.DoctorDetailsRequired);
            }

            // CALL BUSINESS LAYER TO INSERT DOCTOR DETAILS
            var result = await _doctorBL.InsertDoctorDetails(doctorDetails);

            // IF INSERTION FAILED RETURN BAD REQUEST
            if (result != AppConstants.DBResponse.Success)
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.DoctorDetailsAddedSuccessfully);
        }

        [HttpPut]
        [CustomAuth(Roles.Admin, Roles.Doctor)]
        public async Task<IActionResult> UpdateDoctorDetail(DoctorDetails doctorDetails)
        {
            // IF DOCTOR DETAILS IS NULL RETURN BAD REQUEST
            if (doctorDetails == null)
            {
                return BadRequest(AppConstants.ResponseMessages.DoctorDetailsRequired);
            }

            // CALL BUSINESS LAYER TO UPDATE DOCTOR DETAILS
            var result = await _doctorBL.UpdateDoctorDetails(doctorDetails);

            // IF UPDATE FAILED RETURN BAD REQUEST
            if (result != AppConstants.DBResponse.Success)
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.DoctorDetailsUpdatedSuccessfully);
        }

    }
}
