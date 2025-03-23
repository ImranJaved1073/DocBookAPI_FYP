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

        public AccountController(IAccountService authService, IPatientService patientService)
        {
            _authService = authService;
            _patientService = patientService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            AuthResponseDto result = await _authService.RegisterAsync(model);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            if (model.Role == "Patient")
            {
                var user = await _authService.GetUserAsync(model.Email);
                Patient patient = new Patient
                {
                    User = user,
                    UserId = user.Id
                };
                await _patientService.AddPatientAsync(patient);

            }

            else if (model.Role == "Doctor")
            {
                var user = await _authService.GetUserAsync(model.Email);
                Doctor doctor = new Doctor
                {
                    User = user,
                    UserId = user.Id
                };
                await _patientService.AddDoctorAsync(doctor);
            }

            return Ok(result);
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

        [HttpGet("all-doctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var result = await _authService.GetAllDoctors();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _authService.GetUserAsync(id);
            return Ok(result);
        }

        [HttpGet("all-patients")]
        public async Task<List<User>> GetAllPatients()
        {
            return await _authService.GetAllPatients();
        }
    }
}
