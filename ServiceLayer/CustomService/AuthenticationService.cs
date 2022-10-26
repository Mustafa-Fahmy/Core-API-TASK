using AutoMapper;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Data;
using RepositoryLayer.Infrastructure;
using ServiceLayer.ICustomService;
using ServiceLayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceLayer.CustomService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<AuthReturn> Login(Login model)
        {
            AuthReturn authReturn = new AuthReturn() { IsValid = false };
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                authReturn.IsValid = true;
                authReturn.Token = new JwtSecurityTokenHandler().WriteToken(token);
                authReturn.ExpiryDate = token.ValidTo;

            }
            return authReturn;
        }

        public async Task<RegisterReturn> Register(RegisterModel model)
        {

            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return new RegisterReturn() { RegisterEnum = Enum.RegisterEnum.UserAlreadyExists, Status = "Error", Message = "User already exists" };
            }


            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new RegisterReturn() { RegisterEnum = Enum.RegisterEnum.CreationFailed, Status = "Error", Message = "User creation failed! Please check user details and try again." };
            }
            return new RegisterReturn() { RegisterEnum= Enum.RegisterEnum.UserSuccessfullyCreated, Status = "Success", Message = "User created successfully!" };

        }
    }
}
