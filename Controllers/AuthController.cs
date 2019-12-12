using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tennis_Mate.Data;
using Tennis_Mate.Dtos;
using Tennis_Mate.Models;

namespace Tennis_Mate.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repository, DataContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
            _repository = repository;
        }

        [HttpPost("registerInstructor")]

       public async Task<IActionResult> RegisterInstructor(InstructorForRegisterDto instructorForRegisterDto)
        {
            instructorForRegisterDto.Email = instructorForRegisterDto.Email.ToLower();
            if (await _repository.InstructorExists(instructorForRegisterDto.Email))
            {
                return BadRequest("Email is already being used");
            }
            else
            {
                var instructorToCreate = new Instructor
                {
                    Email = instructorForRegisterDto.Email,
                    CurrentEmployer = instructorForRegisterDto.CurrentEmployer,
                    FirstName = instructorForRegisterDto.FirstName,
                    LastName = instructorForRegisterDto.LastName,
                    FullName = instructorForRegisterDto.FirstName + " " + instructorForRegisterDto.LastName,
                    Phone = instructorForRegisterDto.Phone
                };
               var createdInstructor = await _repository.RegisterInstructor(instructorToCreate, instructorForRegisterDto.Password);
                
               return StatusCode(201);
            }
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser(UserForRegisterDto userForRegisterDto)
        {
            // validate request
            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

            if (await _repository.UserExists(userForRegisterDto.Email))
            {
                return BadRequest("Email is already being used");
            }
            var userToCreate = new User
            {
                Email = userForRegisterDto.Email,
                FullName = userForRegisterDto.FirstName + " " + userForRegisterDto.LastName,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Phone = userForRegisterDto.Phone
            };
            var createdUser = await _repository.RegisterUser(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("Login")]

        public async Task<IActionResult> LoginUser(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repository.Login(userForLoginDto.Email, userForLoginDto.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Email, userFromRepo.Email ),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(6),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

    }
}
