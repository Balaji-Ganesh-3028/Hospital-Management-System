using BusinessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.CustomAttributes;

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

        [HttpPost("")]
        [CustomAuth("Patient", "Doctor", "Admin", "Super Admin")]
        public async Task<IActionResult> UserProfile([FromBody] UserProfile userProfile)
        {
            // IF USER PROFILE IS EMPTY
            if (userProfile == null)
            {
                return BadRequest("User details should contain value");
            }

            var result = await _userProfileBL.UserProfile(userProfile);
            Console.WriteLine("User Profile Error: " + result);
            if (result != "Success")
            {
                return StatusCode(500, "An error occurred while updating the user profile.");
            }

            return Ok("User details added successfully");
        }

        [HttpPut("")]
        [CustomAuth("Patient", "Doctor", "Admin", "Super Admin")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfile userProfile)
        {
            // IF USER PROFILE IS EMPTY
            if (userProfile == null)
            {
                return BadRequest("User details should contain value");
            }

            var result = await _userProfileBL.UserProfileUpdate(userProfile);
            Console.WriteLine("User Profile Error: " + result);
            if (result != "Success")
            {
                return StatusCode(500, "An error occurred while updating the user profile.");
            }

            return Ok("User details updated successfully");
        }

        [HttpGet("")]
        [CustomAuth("Front Desk", "Doctor", "Admin")]
        public async Task<IActionResult> GetAllUserDetails([FromQuery] UserDetailsQuery query)
        {
            var result = await _userProfileBL.GetAllUserDetails(query);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [CustomAuth("Patient", "Doctor", "Admin", "Front Desk")]
        public async Task<IActionResult> GetUserProfileDetail(int userId)
        {
            if(userId == 0)
            {
                return BadRequest("userId is required");
            }
            var result = await _userProfileBL.GetUserDetails(userId);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserProfile(int userId)
        {
            if(userId == 0)
            {
                return BadRequest("UserId is required");
            }

            var result = await _userProfileBL.DeleteUserProfile(userId);

            if (result == "Success")
            {
               return Ok(result);
            } else
            {
                return BadRequest("Something went wrong!!!");
            }
        }

    }
}
