using AutoMapper;
using DocBookAPI.DTOs;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<PatientController> _logger;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, ILogger<PatientController> logger, IAppointmentService appointmentService , IMapper mapper)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatientAsync(PatientDTO patient)
        {
            try
            {
                var addedPatient = await _patientService.AddPatientAsync(patient);
                return Ok(addedPatient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding patient");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding patient");
            }
        }

        [HttpDelete("DeletePatient")]
        public async Task<IActionResult> DeletePatientAsync(int id)
        {
            try
            {
                var isDeleted = await _patientService.DeletePatientAsync(id);
                if (isDeleted)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting patient");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting patient");
            }
        }

        [HttpGet("GetPatients")]
        public async Task<IActionResult> GetAllPatientsAsync()
        {
            try
            {
                var patients = await _patientService.GetAllPatientsAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting patients");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting patients");
            }
        }

        [HttpGet("GetPatientById/{Id}")]
        public async Task<IActionResult> GetPatientByIdAsync(int id)
        {
            try
            {
                var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient != null)
                {
                    return Ok(patient);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting patient");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting patient");
            }
        }

        [HttpGet("GetPatientByEmail/{email}")]
        public async Task<IActionResult> GetPatientByEmailAsync(string email)
        {
            try
            {
                var patient = await _patientService.GetPatientByEmailAsync(email);
                if (patient != null)
                {
                    return Ok(patient);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting patient by email");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting patient by email");

        }

        [HttpGet("GetPatientByUserName{userName}")]
        public async Task<IActionResult> GetPatientByUserNameAsync(string userName)
        {
            try
            {
                var patient = await _patientService.GetPatientByUserNameAsync(userName);
                if (patient != null)
                {
                    return Ok(patient);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting patient by username");
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting patient by username");
        }

        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> UpdatePatientAsync([FromBody] PatientDTO patient)
        {
            try
            {
                var updatedPatient = await _patientService.UpdatePatientAsync(patient);
                return Ok(updatedPatient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating patient");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating patient");
            }
        }

        //create appointment
        //[HttpPost("CreateAppointment")]
        //public async Task<IActionResult> CreateAppointmentAsync([FromBody] Appointment appointment)
        //{
        //    try
        //    {
        //        var createdAppointment = await _appointmentService.CreateAppointment(appointment);
        //        return Ok(createdAppointment);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error creating appointment");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error creating appointment");
        //    }
        //}

        ////get all appointments
        //[HttpGet("GetAppointments")]
        //public async Task<IActionResult> GetAppointmentsAsync()
        //{
        //    try
        //    {
        //        var appointments = await _appointmentService.GetAppointments();
        //        return Ok(appointments);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting appointments");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error getting appointments");
        //    }
        //}

        ////get appointment by patient id
        //[HttpGet("GetAppointmentsByPatientId/{patientId}")]
        //public async Task<IActionResult> GetAppointmentsByPatientIdAsync(int patientId)
        //{
        //    try
        //    {
        //        var appointments = await _appointmentService.GetAppointmentsByPatient(patientId);
        //        return Ok(appointments);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting appointments by patient id");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error getting appointments by patient id");
        //    }
        //}

        ////cancel appointment
        //[HttpPost("CancelAppointment/{id}")]
        //public async Task<IActionResult> CancelAppointmentAsync(int id)
        //{
        //    try
        //    {
        //        var appointment = await _appointmentService.CancelAppointment(id);
        //        if (appointment)
        //        {
        //            return Ok(appointment);
        //        }
        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error cancelling appointment");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error cancelling appointment");
        //    }
        //}
    }
}
