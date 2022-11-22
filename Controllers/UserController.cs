using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Todolistapplication.Models;
using Todolistapplication.Interface;
using System.Text;
using AutoMapper;
using Todolistapplication.SecureUtility;

namespace Todolistapplication.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TodolistDbContext _todolistDbContext;
        
        private readonly IUser _Iuser;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config, TodolistDbContext context, IUser iuser,IMapper mapper)
        {
            _configuration = config;
            _todolistDbContext = context;
            _Iuser = iuser;
            _mapper = mapper;
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> Register(string userEmail, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            User existingUserByEmail = await _Iuser.GetByEmail(userEmail);
            
            if (existingUserByEmail != null)
            {
                return Conflict();
            }
          

            //Create User
            User registrationUser = new User()
            {
                Email = userEmail,
                Password=password,
                CreatedDate = DateTime.UtcNow
               
            };
            
            await _Iuser.Create(registrationUser);
            return Ok();
        }
        
        [HttpPost("signin")]
        public async Task<IActionResult> SignInUser(string userEmail, string password)
        {
            if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(password))
            {
                var user = await GetUser(userEmail, password);
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

                return BadRequest("Invalid credentials");
            }

            return BadRequest();
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(string userEmail, string password)
        {
            if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(password))
            {
                var user = await GetUserByEmail(userEmail);

                if (user != null)
                {
                    if (user.PasswordHash.SequenceEqual(password.GetPasswordHash()))
                        return BadRequest("Same password is already in use for the user");

                    user.Password = password;
                    user.UpdatedDate = DateTime.UtcNow;

                    _todolistDbContext.Entry(user).State = EntityState.Modified;
                    await _todolistDbContext.SaveChangesAsync();

                    return Ok();
                }

                return BadRequest();
            }

            return BadRequest();
        }



        //public async Task<IActionResult> ChangePassword(string userEmail, string password)
        //{
        //    if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(password))
        //    {
        //        var user = await GetUser(userEmail, password);

        //        if (user != null)
        //        {
        //            if (user.PasswordHash.ToHexString() == password.GetPasswordHash().ToHexString())
        //                return BadRequest("Same password is already in use for the user");

        //            user.Password = password;
        //            user.UpdatedDate = DateTime.UtcNow;

        //            _todolistDbContext.Entry(user).State = EntityState.Modified;
        //            await _todolistDbContext.SaveChangesAsync();

        //            return Ok();
        //        }

        //        return BadRequest();
        //    }

        //    return BadRequest();
        //}

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

            //var passwordHash = password.GetPasswordHash().ToHexString();
            var passwordHash = password.GetPasswordHash();

            return await _todolistDbContext?.userInfos?.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash.SequenceEqual(passwordHash))! ?? throw new InvalidOperationException("Wrong email or password");

           //return await _todolistDbContext?.userInfos?.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash.ToHexString() == passwordHash)! ?? throw new InvalidOperationException();
        }
        private async Task<User>? GetUserByEmail(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException("email");
            }
            

            return await _todolistDbContext?.userInfos?.FirstOrDefaultAsync(u => u.Email == email)! ?? throw new InvalidOperationException("Email doesn't exist");
        }
    }
}
