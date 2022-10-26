using AutoMapper;
using DomainLayer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Data;
using ServiceLayer.ICustomService;
using ServiceLayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APITask.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticateController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            AuthReturn authReturn = await _authenticationService.Login(model);
            if (authReturn.IsValid)
            {
                return Ok(new
                {
                    token = authReturn.Token,
                    expiration = authReturn.ExpiryDate
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            RegisterReturn registerReturn = await _authenticationService.Register(model);

            if (registerReturn.RegisterEnum == ServiceLayer.Enum.RegisterEnum.UserAlreadyExists)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = registerReturn.Status, Message = registerReturn.Message });
            }
            else if (registerReturn.RegisterEnum == ServiceLayer.Enum.RegisterEnum.CreationFailed)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = registerReturn.Status, Message = registerReturn.Message });
            }
            else
            {

                return Ok(new Response { Status = registerReturn.Status, Message = registerReturn.Message });
            }
        }
    }
}
