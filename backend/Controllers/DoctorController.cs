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
            var result = await _doctorBL.GetAllDoctorDetails();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [CustomAuth(Roles.Admin, Roles.FrontDesk, Roles.Doctor, Roles.Patient)]
        public async Task<IActionResult> GetDoctor(int userId)
        {
            var result = await _doctorBL.GetDoctorDetails(userId);
            return Ok(result);
        }

        [HttpPost]
        [CustomAuth(Roles.Admin, Roles.Doctor)]
        public async Task<IActionResult> InsertDoctorDetail(DoctorDetails doctorDetails)
        {
            if (doctorDetails == null)
            {
                return BadRequest(AppConstants.ResponseMessages.DoctorDetailsRequired);
            }

            var result = await _doctorBL.InsertDoctorDetails(doctorDetails);

            if (result == AppConstants.DBResponse.Success) return Ok(AppConstants.ResponseMessages.DoctorDetailsAddedSuccessfully);
            else return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
        }

        [HttpPut]
        [CustomAuth(Roles.Admin, Roles.Doctor)]
        public async Task<IActionResult> UpdateDoctorDetail(DoctorDetails doctorDetails)
        {
            if (doctorDetails == null)
            {
                return BadRequest(AppConstants.ResponseMessages.DoctorDetailsRequired);
            }

            var result = await _doctorBL.UpdateDoctorDetails(doctorDetails);

            if (result == AppConstants.DBResponse.Success) return Ok(AppConstants.ResponseMessages.DoctorDetailsUpdatedSuccessfully);
            else return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
        }

    }
}
