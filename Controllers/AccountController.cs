using DocBookAPI.DTOs;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _authService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public AccountController(IAccountService authService, IPatientService patientService, IDoctorService doctorService)
        {
            _authService = authService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            if (model.Role.ToLower() == "patient")
            {
                var user = await _authService.GetUserAsync(model.Email);
                PatientDTO patient = new PatientDTO
                {
                    UserId = user.Id
                };
                await _patientService.AddPatientAsync(patient);

            }

            else if (model.Role.ToLower() == "doctor")
            {
                var user = await _authService.GetUserAsync(model.Email);
                DoctorDTO doctor = new DoctorDTO
                {
                    UserId = user.Id
                };
                await _doctorService.AddDoctorAsync(doctor);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.AuthenticateUserAsync(model);
            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _authService.GetAllUsersAsync();
            return Ok(result);
        }

        //get role from token
        [HttpGet("role")]
        public async Task<IActionResult> GetRole(string email)
        {
            var userRole = await _authService.GetUserRoleAsync(email);
            if (userRole == null)
            {
                return NotFound();
            }
            return Ok(new { role = userRole });
        }


        //[HttpGet("all-doctors")]
        //public async Task<IActionResult> GetAllDoctors()
        //{
        //    var result = await _authService.GetAllDoctors();
        //    return Ok(result);
        //}

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await _authService.GetUserAsync(id);
            return Ok(result);
        }

        //[HttpGet("all-patients")]
        //public async Task<List<User>> GetAllPatients()
        //{
        //    return await _authService.GetAllPatients();
        //}
    }
}
