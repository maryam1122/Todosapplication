using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Todolistapplication.Models;
using System.Text;


namespace Todolistapplication.Controllers
{
    [Route("api/Signin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly TodolistDbContext _todolistDbContext;

        public UserController(IConfiguration config, TodolistDbContext context)
        {
            _configuration = config;
            _todolistDbContext = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(user_info _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);
                if(user != null)
                {   
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", user.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }




        }

        private async Task<user_info> GetUser(string email , string password)
        {
            return await _todolistDbContext.user_infos.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }



    }
}
