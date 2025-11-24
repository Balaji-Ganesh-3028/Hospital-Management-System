using AppModels.Models;
using AppModels.RequestModels;
using backend.CustomAttributes;
using backend.Enum;
using BusinessLayer.Interface;
using Constant.Constants;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileBL _userProfileBL;
        public UserController(IUserProfileBL userProfileBL)
        {
            _userProfileBL = userProfileBL;
        }

        [HttpPost]
        [CustomAuth(Roles.Patient, Roles.Doctor, Roles.Admin)]
        public async Task<IActionResult> UserProfile([FromBody] UserProfileRequest userProfile)
        {
            // IF USER PROFILE IS EMPTY
            if (userProfile == null)
            {
                return BadRequest(AppConstants.ResponseMessages.UserDetailsReuqired);
            }

            // CALL BUSINESS LAYER TO INSERT USER PROFILE
            var result = await _userProfileBL.UserProfile(userProfile);

            // IF INSERTION FAILED RETURN SERVER ERROR
            if (result != AppConstants.DBResponse.Success)
            {
                return StatusCode(500, AppConstants.ResponseMessages.InsertUserErrorMessage);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.UserDetailsAddedSuccessfully);
        }

        [HttpPut]
        [CustomAuth(Roles.Patient, Roles.Doctor, Roles.Admin)]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileRequest userProfile)
        {
            // IF USER PROFILE IS EMPTY
            if (userProfile == null)
            {
                return BadRequest(AppConstants.ResponseMessages.UserDetailsReuqired);
            }

            // CALL BUSINESS LAYER TO UPDATE USER PROFILE
            var result = await _userProfileBL.UserProfileUpdate(userProfile);

            // IF UPDATE FAILED RETURN SERVER ERROR
            if (result != AppConstants.DBResponse.Success)
            {
                return BadRequest(AppConstants.ResponseMessages.UpdateUserErrorMessage);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.UserDetailsUpdatedSuccessfully);
        }

        [HttpGet]
        [CustomAuth(Roles.FrontDesk, Roles.Doctor, Roles.Admin)]
        public async Task<IActionResult> GetAllUserDetails([FromQuery] UserDetailsQuery query)
        {
            // CALL BUSINESS LAYER TO GET ALL USER DETAILS
            var result = await _userProfileBL.GetAllUserDetails(query);
            
            // IF RESULT IS NULL RETURN NOT FOUND
            if (result == null)
            {
                return Ok(AppConstants.ResponseMessages.NoUserDetailsFound);
            }

            // RETURN USER DETAILS
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [CustomAuth(Roles.Patient, Roles.Doctor, Roles.Admin, Roles.FrontDesk)]
        public async Task<IActionResult> GetUserProfileDetail(int userId)
        {
            // IF USER ID IS LESS THAN OR EQUAL TO ZERO RETURN BAD REQUEST
            if(userId <= 0)
            {
                return BadRequest(AppConstants.ResponseMessages.UserIdRequired);
            }

            // CALL BUSINESS LAYER TO GET USER DETAILS BY USER ID
            var result = await _userProfileBL.GetUserDetails(userId);
            // IF NO USER DETAILS FOUND RETURN NOT FOUND
            if (result == null)
            {
                return NotFound(AppConstants.ResponseMessages.UserDetailNotFound);
            }

            // RETURN USER DETAILS
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        [CustomAuth(Roles.Admin)]
        public async Task<IActionResult> DeleteUserProfile(int userId)
        {
            // IF USER ID IS LESS THAN OR EQUAL TO ZERO RETURN BAD REQUEST
            if(userId <= 0)
            {
                return BadRequest(AppConstants.ResponseMessages.UserIdRequired);
            }

            // CALL BUSINESS LAYER TO DELETE USER PROFILE
            var result = await _userProfileBL.DeleteUserProfile(userId);

            // IF DELETION FAILED RETURN BAD REQUEST
            if (result != AppConstants.DBResponse.Success)
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.UserDeletedSuccessfully);
        }

    }
}
