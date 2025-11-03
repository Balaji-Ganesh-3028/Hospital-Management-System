using BusinessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("UserController is working!");
        }

        [HttpPost("profile")]
        public async Task<IActionResult> UserProfile([FromBody] UserProfile userProfile)
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

            return Ok("User details added successfully");
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var result = await _userProfileBL.GetAllUserDetails();
            return Ok(result);
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> GetUserProfileDetail([FromQuery] int userId)
        {
            if(userId == 0)
            {
                return BadRequest("userId is required");
            }
            var result = await _userProfileBL.GetUserDetails(userId);
            return Ok(result);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUserProfile([FromQuery] int userId)
        {
            if(userId == 0)
            {
                return BadRequest("UserId is required");
            }

            var result = await _userProfileBL.DeleteUserProfile(userId);
            return Ok(result);
        }

    }
}
