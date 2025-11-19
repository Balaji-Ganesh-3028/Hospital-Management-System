using BusinessLayer.Interface;
using Constant.Constants;
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
                return BadRequest(AppConstants.ResponseMessages.UserDetailsReuqired);
            }

            // The service method should be refactored to return Task instead of Task<string>
            await _registerService.RegisterUser(request);

            return Ok(AppConstants.ResponseMessages.UserRegisteredSuccessfully);
        }
    }
}
