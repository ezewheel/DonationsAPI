using Application.Models.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto<string>>> Login([FromBody] LoginRequestDto request)
        {
            string token = await _authService.LoginAsync(request);
            ResponseDto<string> response = new ResponseDto<string>();
            if (token == null)
            {
                response.Status = 401;
                response.Message = "failed";
                response.Data = null;
                return Unauthorized(response);
            }

            response.Status = 200;
            response.Message = "success";
            response.Data = token;
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<string> Register(RegistrationRequestDto request)
        {
            return await _authService.RegisterAsync(request);
        }
    }
}
