using BusinessLayer.Interface;
using DataAccessLayer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{

    public class RegisterController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IRegisterService _registerService;
        public RegisterController(IConfiguration congiguration, IRegisterService registerService)
        {
            _configuration = congiguration;
            _registerService = registerService;
        }

        [AllowAnonymous]
        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest("User details required");
            }

            var result = await _registerService.RegisterUser(request);

            if (result != "Success")
            {
                if (result.StartsWith("Database error: "))
                {
                    return StatusCode(500, result);
                }
                else if (result.StartsWith("Internal server error: "))
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return BadRequest("Registration failed, Something went wrong!!");
                }
            }
            else
            {
                return StatusCode(200, "User registered successfully");
            }

        }
    }
}
