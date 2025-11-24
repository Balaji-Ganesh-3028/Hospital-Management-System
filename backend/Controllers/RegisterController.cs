using AppModels.RequestModels;
using BusinessLayer.Interface;
using Constant.Constants;
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
            // IF REQUEST IS NULL RETURN BAD REQUEST
            if (request == null)
            {
                return BadRequest(AppConstants.ResponseMessages.UserDetailsReuqired);
            }

            // CALL BUSINESS LAYER TO REGISTER USER
            var result = await _registerService.RegisterUser(request);

            // IF REGISTRATION FAILED RETURN BAD REQUEST
            if (result != AppConstants.DBResponse.Success)
            {
                return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
            }

            // RETURN SUCCESS MESSAGE
            return Ok(AppConstants.ResponseMessages.UserRegisteredSuccessfully);
        }
    }
}
