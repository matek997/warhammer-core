using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WarhammerCore.Data.Models;
using WarhammerCore.WebApi.Models.Request;
using WarhammerCore.WebApi.Models.Response;

namespace WarhammerCore.WebApi.Controllers
{
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private IConfiguration _config;

        public UsersController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        //[HttpPost]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest login)
        {
            ActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new LoginResponse(tokenString));
            }

            return response;
        }
       [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult> Test()
        {

            return Ok(new { res = "yay!" });
            
        }


        private string GenerateJSONWebToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(LoginRequest login)
        {
            UserModel user = null;

            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    
            if (login.Email == "example@example.com")
            {
                user = new UserModel { Email = login.Email };
            }
            return user;
        }
    }
}
