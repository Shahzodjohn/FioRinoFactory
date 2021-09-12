using FioRinoFactory.Data;
using FioRinoFactory.DTOs;
using FioRinoFactory.Helper;
using FioRinoFactory.Models;
using FioRinoFactory.Repositories;
using FioRinoFactory.ServiceTEST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FioRinoFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _JwtService;
        private readonly FioAndRinoContext _context;
        private readonly IRegisterService _service;

        public AuthController(IUserRepository repository, JwtService jwtService, FioAndRinoContext context, IRegisterService service)
        {
            _repository = repository;
            _JwtService = jwtService;
            _context = context;
            _service = service;
        }
        [HttpPost("RegistrationUser")]
        public IActionResult RegistrationUser(RegisterDTO dto)
        {
            //var sir = _service.RegistrationUser(dto);
            // if(sir != null)
            // {
            //     return BadRequest(new { message = "This email is already used!" });
            // }

            // return Created("Success", _service.RegistrationUser(dto));
            #region return this back

            //var user = new DmUser
            //{
            //    FirstName = dto.FirstName,
            //    LastName = dto.LastName,
            //    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            //    Email = dto.Email,
            //    PhoneNumber = dto.PhoneNumber,
            //    RoleId = dto.RoleId,
            //    PositionId = dto.PositionId
            //};
            //var isusedemail = _repository.GetByEmail(dto.Email);
            //if (isusedemail != null)
            //{
            //    return BadRequest(new { message = "this email is already used!" });
            //}

            //return Created("success", _repository.Create(user));
            #endregion
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDTO dto)
        {
            var User = _repository.GetByEmail(dto.Email);
            if (User == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, User.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }
            var jwt = _JwtService.Generate(User.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });
            return Ok(new { message = "success" });
        }
        [HttpGet("GetUser")]
        public IActionResult User()
        {
            try
            {
                var Jwt = Request.Cookies["jwt"];
                var token = _JwtService.Verify(Jwt);
                var userId = int.Parse(token.Issuer);
                var user = _repository.GetById(userId);
                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Success" });
        }
    }
}
