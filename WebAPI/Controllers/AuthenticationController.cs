﻿using Application;
using Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Presentation.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [Route("authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public AuthenticationController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLogin loginData)
        {
            if (string.IsNullOrEmpty(loginData.Username))
            {
                return BadRequest("Please input a valid username.");
            }
            if (string.IsNullOrEmpty(loginData.Password))
            {
                return BadRequest("Please input a valid password.");
            }

            var request = new LoginRequest
            { 
                Username = loginData.Username,
                Password = loginData.Password
            };

            var user = _userService.ValidateUser(request);

            if (user != null)
            {
                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
                var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);
                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", user.Id.ToString()));

                var jwtSecurityToken = new JwtSecurityToken(
                    _config["Authentication:Issuer"],
                    _config["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signature
                );

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                ResponseForLogin loginRsp = new ResponseForLogin()
                {
                    Status = 200,
                    Message = "User authenticated successfully.",
                    Token = tokenToReturn
                };

                return Ok(loginRsp);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
