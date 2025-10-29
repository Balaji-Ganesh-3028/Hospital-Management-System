using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("UserController is working!");
        }

        [HttpPost("profile")]
        public async Task<IActionResult> UserProfile()
        {
            return Ok("Success");
        }

    }
}
