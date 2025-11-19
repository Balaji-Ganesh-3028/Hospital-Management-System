using BusinessLayer.Interface;
using DataAccessLayer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{

    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;
        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [AllowAnonymous]
        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest("User details are required.");
            }

            // The service method should be refactored to return Task instead of Task<string>
            await _registerService.RegisterUser(request);

            // On success, return a 201 Created status with a success message.
            // If RegisterUser throws an exception, the ExceptionHandler middleware will catch it.
            return StatusCode(201, "User registered successfully");
        }
    }
}
