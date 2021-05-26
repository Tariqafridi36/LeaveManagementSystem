using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using LMS.API.Data;
 
using LMS.API.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using LMS.API.Dtos;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _config;
        private readonly ILogger<LeaveController> _logger;
        private readonly IBaseResponse _response;

        public AuthController(AppDBContext appDBContext, IConfiguration config, ILogger<LeaveController> logger, IBaseResponse response)
        {
            _dbcontext = appDBContext;
            _config = config;
            _logger = logger;
            _response = response;
        }

        [HttpPost("login")]
        public async Task<IBaseResponse> Login([FromBody] AppUser user)
        {

            try
            {
                var loginUser = await _dbcontext.AppUsers.Where(x => x.Email == user.UserName && x.Password == user.Password && x.IsActive == true).FirstOrDefaultAsync();

                if (loginUser != null)
                {
                    var key = _config.GetSection("AppSettings:AppKey").Value;
                    _response.token = GenerateToken(loginUser.UserName, loginUser.UserId.ToString(), loginUser.Email, key);
                    _response.desc = "Logged In";
                }
            }
            catch (Exception ex)
            {
                _response.code = "01";
                _logger.LogError(ex.Message); 
            }

            return _response;

        }


        private string GenerateToken(string UserName, string UserId, string email, string AppKey)
        {
            try
            {
                var claims = new[]
                   {
                new Claim(ClaimTypes.NameIdentifier, UserId),
                new Claim(ClaimTypes.Name, UserName),
                new Claim("email", email)

            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppKey));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(60),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var CreatedToken = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(CreatedToken);
            }
            catch (Exception)
            {
                return "";
            }
        } 
    }
}