using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Office.Infrastructure.Jwt;
using Office.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Office.Context;
using Office.Context.Dtos;

namespace Office.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        private readonly TokenManagement _tokenManagement;
        public AccountController(ILogger<AccountController> logger, IUserService userService, TokenManagement tokenManagement)
        {
            _logger = logger;
            _userService = userService;
            _tokenManagement = tokenManagement;
        }

        /// <summary>
        /// JWT login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            if (!_userService.IsValidUser(request.UserName, request.Password))
            {
                return BadRequest("Invalid Request");
            }
            var user = _userService.Login(request);
            if (user.Id <= 0)
            {
                return BadRequest("Invalid username or password");
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Role,user.Role.Name),
                new Claim(ClaimTypes.Name,user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);


            var jwt = token;
            var handler = new JwtSecurityTokenHandler();
            var tokenz = handler.ReadJwtToken(jwt);
           


            _logger.LogInformation($"User [{request.UserName}] logged in the system.");
            return Ok(new LoginResult
            {
                UserName = request.UserName,
                JwtToken = token
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }
            else 
            {
                _userService.AddUser(request);
            }
            return Ok();

        }
    }
}
