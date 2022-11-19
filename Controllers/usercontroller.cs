using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Todolistapplication.Models;
using Todolistapplication.Interface;
using System.Text;
using AutoMapper;

namespace Todolistapplication.Controllers
{

    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly TodolistDbContext _todolistDbContext;
        public static User user = new User();
        private readonly IUser _Iuser;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config, TodolistDbContext context, IUser Iuser,IMapper mapper)
        {
            _configuration = config;
            _todolistDbContext = context;
            _Iuser = Iuser;
            _mapper = mapper;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody]User registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User existingUserByEmail = await _Iuser.GetByEmail(registerRequest.Email);
            if(existingUserByEmail != null)
            {
                return Conflict();
            }
            //Create User
            User registrationUser = new User()
            {
                Email = registerRequest.Email,
                Password = registerRequest.Password
            };
          await _Iuser.Create(registrationUser);
            return Ok();


        }
        [HttpPost("signin")]
        public async Task<IActionResult> Post(User _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);
                if (user != null)
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

        private async Task<User>? GetUser(string email, string password)
        {
            if (email is null)
            {
                throw new ArgumentNullException("email");
            }

            if (password is null)
            {
                throw new ArgumentNullException("password");
            }
            return await _todolistDbContext.userInfos.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }





    }
}
