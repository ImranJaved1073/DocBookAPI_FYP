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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IAccountService _accountService;
        private readonly IAppointmentService _appointmentService;
        private readonly IReviewService _reviewService;
        private readonly ILogger<DoctorController> _logger;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, ILogger<DoctorController> logger, IMapper mapper, IAccountService accountService, IAppointmentService appointmentService, IReviewService reviewService)
        {
            _doctorService = doctorService;
            _logger = logger;
            _mapper = mapper;
            _accountService = accountService;
            _appointmentService = appointmentService;
            _reviewService = reviewService;
        }

        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctorAsync(DoctorDTO doctor)
        {
            try
            {
                var addedDoctor = await _doctorService.AddDoctorAsync(doctor);
                return Ok(addedDoctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding doctor");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding doctor");
            }
        }

        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctorAsync(int id)
        {
            try
            {
                var isDeleted = await _doctorService.DeleteDoctorAsync(id);
                if (isDeleted)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting doctor");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting doctor");
            }
        }

        [HttpGet("GetDoctors")]
        public async Task<IActionResult> GetAllDoctorsAsync()
        {
            try
            {
                var doctors = await _doctorService.GetAllDoctorsAsync();
                doctors = doctors.Select(d =>
                {
                    d.User = _accountService.GetUserAsync(d.UserId).Result;
                    d.Appointments = _appointmentService.GetAppointmentsByDoctor(d.Id).Result;
                    d.Reviews = _reviewService.GetReviewsByDoctorId(d.Id).Result;
                    return d;
                }).ToList();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all doctors");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting all doctors");
            }
        }

        [HttpGet("GetDoctorById/{id}")]
        public async Task<IActionResult> GetDoctorByIdAsync(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);
                if (doctor != null)
                {
                    doctor.User= await _accountService.GetUserAsync(doctor.UserId);
                    doctor.Appointments = await _appointmentService.GetAppointmentsByDoctor(doctor.Id);
                    doctor.Reviews = await _reviewService.GetReviewsByDoctorId(doctor.Id);
                    return Ok(doctor);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting doctor by id");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting doctor by id");
            }
        }

        [HttpGet("GetDoctorByEmail/{email}")]
        public async Task<IActionResult> GetDoctorByEmailAsync(string email)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByEmailAsync(email);
                if (doctor != null)
                {
                    doctor.User = await _accountService.GetUserAsync(doctor.UserId);
                    doctor.Appointments = await _appointmentService.GetAppointmentsByDoctor(doctor.Id);
                    doctor.Reviews = await _reviewService.GetReviewsByDoctorId(doctor.Id);
                    return Ok(doctor);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting doctor by email");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting doctor by email");
            }
        }

        [HttpGet("GetDoctorByUserName/{userName}")]
        public async Task<IActionResult> GetDoctorByUserNameAsync(string userName)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByUserNameAsync(userName);
                if (doctor != null)
                {
                    doctor.User = await _accountService.GetUserAsync(doctor.UserId);
                    doctor.Appointments = await _appointmentService.GetAppointmentsByDoctor(doctor.Id);
                    doctor.Reviews = await _reviewService.GetReviewsByDoctorId(doctor.Id);
                    return Ok(doctor);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting doctor by username");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting doctor by username");
            }
        }

        [HttpPut("UpdateDoctor/{doctorId}")]
        public async Task<IActionResult> UpdateDoctorAsync(int doctorId, [FromBody] DoctorDTO doctor)
        {
                var updatedDoctor = await _doctorService.UpdateDoctorAsync(doctorId,doctor);
                return Ok(updatedDoctor);
        }

        // update availability
        [Authorize(Roles = "Doctor")]
        [HttpPut("UpdateAvailability/{doctorId}")]
        public async Task<IActionResult> UpdateAvailabilityAsync(int doctorId, [FromBody] string availability)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(doctorId);
                if (doctor != null)
                {
                    doctor.Availability = availability;
                    var doctorDTO = _mapper.Map<DoctorDTO>(doctor);
                    var updatedDoctor = await _doctorService.UpdateDoctorAsync(doctorId, doctorDTO);
                    return Ok(updatedDoctor);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating availability");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating availability");
            }
        }

    }
}
