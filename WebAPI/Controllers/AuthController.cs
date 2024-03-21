using Microsoft.AspNetCore.Mvc;
using PublicWebSite;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;

        public AuthController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var response = _authProvider.GenerateJwtToken(login);

            if (response.Success)
            {
                return Ok(new { Token = response.Token, Message = "Success" });
            }

            return Unauthorized();
        }
    }

}
