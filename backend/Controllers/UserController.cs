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

            var result = await _userProfileBL.UserProfile(userProfile);
            Console.WriteLine("User Profile Error: " + result);
            if (result != AppConstants.DBResponse.Success)
            {
                return StatusCode(500, AppConstants.ResponseMessages.InsertUserErrorMessage);
            }

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

            var result = await _userProfileBL.UserProfileUpdate(userProfile);
            Console.WriteLine("User Profile Error: " + result);
            if (result != AppConstants.DBResponse.Success)
            {
                return StatusCode(500, AppConstants.ResponseMessages.UpdateUserErrorMessage);
            }

            return Ok(AppConstants.ResponseMessages.UserDetailsUpdatedSuccessfully);
        }

        [HttpGet]
        [CustomAuth(Roles.FrontDesk, Roles.Doctor, Roles.Admin)]
        public async Task<IActionResult> GetAllUserDetails([FromQuery] UserDetailsQuery query)
        {
            var result = await _userProfileBL.GetAllUserDetails(query);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [CustomAuth(Roles.Patient, Roles.Doctor, Roles.Admin, Roles.FrontDesk)]
        public async Task<IActionResult> GetUserProfileDetail(int userId)
        {
            if(userId == 0)
            {
                return BadRequest(AppConstants.ResponseMessages.UserIdRequired);
            }
            var result = await _userProfileBL.GetUserDetails(userId);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        [CustomAuth(Roles.Admin)]
        public async Task<IActionResult> DeleteUserProfile(int userId)
        {
            if(userId == 0)
            {
                return BadRequest(AppConstants.ResponseMessages.UserIdRequired);
            }

            var result = await _userProfileBL.DeleteUserProfile(userId);

            if (result == AppConstants.DBResponse.Success)
            {
               return Ok(result);
            } else
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }
        }

    }
}
