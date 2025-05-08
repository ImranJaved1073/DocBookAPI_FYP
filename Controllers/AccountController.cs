using AutoMapper;
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
        private readonly IMapper _mapper;

        public AccountController(IAccountService authService, IPatientService patientService, IDoctorService doctorService, IMapper mapper)
        {
            _authService = authService;
            _patientService = patientService;
            _doctorService = doctorService;
            _mapper = mapper;
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

        [HttpGet("verify-email/{email}")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            var user = await _authService.GetUserAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new { email = user.Email });
        }

        // reset password
        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword(LoginDto logindto)
        {
            var user = await _authService.GetUserAsync(logindto.Email!);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _authService.ResetPasswordAsync(user, logindto.Password!);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok(new { success = true });


        }


        //[HttpGet("all-doctors")]
        //public async Task<IActionResult> GetAllDoctors()
        //{
        //    var result = await _authService.GetAllDoctors();
        //    return Ok(result);
        //}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await _authService.GetUserByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("updateProfile/{userId}")]
        public async Task<IActionResult> UpdateProfile(string userId, [FromBody] ProfileDTO model)
        {
            var result = await _authService.UpdateUserProfileAsync(userId,model);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //[HttpPut("UpdateProfile")]
        //public async Task<IActionResult> UpdateProfile([FromBody] ProfileDTO model)
        //{
        //    var doctor = await _doctorService.GetDoctorByIdAsync(model.Id);
        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }
        //    doctor.Name = model.Name;
        //    doctor.Bio = model.Bio;
        //    doctor.Hospital = model.Hospital;
        //    doctor.Specialization = model.Specialization;
        //    doctor.Qualification = model.Qualification;
        //    doctor.ExperienceYears = model.ExperienceYears;
        //    doctor.ConsultationFee = model.ConsultationFee;
        //    doctor.Availability = model.Availability;
        //    var doctordto = _mapper.Map<DoctorDTO>(doctor);
        //    await _doctorService.UpdateDoctorAsync(doctordto);

        //    ApplicationUser user = await _authService.GetUserAsync(model.UserId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    user.ProfilePicture = model.ImageUrl;
        //    user.DateOfBirth = model.dateOfBirth;
        //    user.PhoneNumber = model.PhoneNumber;
        //    user.Address = model.Address;
        //    await _authService.UpdateUserProfileAsync(user);

        //    return Ok();
        //}



        //[HttpGet("all-patients")]
        //public async Task<List<User>> GetAllPatients()
        //{
        //    return await _authService.GetAllPatients();
        //}
    }
}
