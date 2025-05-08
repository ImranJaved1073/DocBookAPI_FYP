using DocBookAPI.DTOs;
using DocBookAPI.Helpers;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public AppointmentsController(IAppointmentService appointmentService, IPatientService patientService, IDoctorService doctorService)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        [HttpGet("GetAppointments")]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _appointmentService.GetAppointments();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointment(id);
            return Ok(appointment);
        }

        [HttpPost("BookAppointment")]
        public async Task<IActionResult> BookAppointment(AppointmentDTO appointment)
        {
            var createdAppointment = await _appointmentService.CreateAppointment(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.Id }, createdAppointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }
            var updatedAppointment = await _appointmentService.UpdateAppointment(appointment);
            return Ok(updatedAppointment);
        }
        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointment(id);
            if (appointment == null)
            {
                return NotFound();
            }
            await _appointmentService.DeleteAppointment(id);
            return NoContent();
        }

        [HttpPut("CancelAppointment/{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            await _appointmentService.CancelAppointment(id);
            return NoContent();
        }

        [HttpPut("ApproveAppointment/{id}")]
        public async Task<IActionResult> ApproveAppointment(int id)
        {
            var appointment = await _appointmentService.ApproveAppointment(id);
            if (appointment == null)
            {
                //appointment already approved
                return BadRequest("Appointment already approved for other patient so cancel this appointment");
            }
            return NoContent();
        }

        [HttpGet("byPatient/{patientId}")]
        public async Task<IActionResult> GetAppointmentsByPatient(int patientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByPatient(patientId);
            return Ok(appointments);
        }

        [HttpGet("byDoctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctor(int doctorId)
        {
            var appointments = await _appointmentService.GetAppointmentsByDoctor(doctorId);
            return Ok(appointments);
        }

        [HttpGet("byPatientEmail/{patientEmail}")]
        public async Task<IActionResult> GetAppointmentsByPatientEmail(string patientEmail)
        {
            var patient = await _patientService.GetPatientByEmailAsync(patientEmail);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }
            var appointments = await _appointmentService.GetAppointmentsByPatient(patient.Id);
            return Ok(appointments);
        }

        [HttpGet("byDoctorEmail/{doctorEmail}")]
        public async Task<IActionResult> GetAppointmentsByDoctorEmail(string doctorEmail)
        {
            var doctor = await _doctorService.GetDoctorByEmailAsync(doctorEmail);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }
            var appointments = await _appointmentService.GetAppointmentsByDoctor(doctor.Id);
            return Ok(appointments);
        }

        [HttpGet("byDate/{doctorEmail}")]
        public async Task<IActionResult> GetAppointmentsByDate(string doctorEmail, DateTime date)
        {
            var doctor = await _doctorService.GetDoctorByEmailAsync(doctorEmail);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }
            var appointments = await _appointmentService.GetAppointmentsByDate(doctor.Id,date);
            return Ok(appointments);
        }

        [HttpGet("GetBookedSlots")]
        public async Task<IActionResult> GetBookedSlots(int doctorId, DateTime appointmentDate)
        {
            var bookedSlots = await _appointmentService.GetBookedSlots(doctorId, appointmentDate);
            return Ok(bookedSlots);
        }

        [HttpGet("GetPatientBookedSlots")]
        public async Task<IActionResult> GetPatientBookedSlots(int patientId, int doctorId, DateTime appointmentDate)
        {
            var bookedSlots = await _appointmentService.GetPatientBookedSlots(patientId, doctorId, appointmentDate);
            return Ok(bookedSlots);
        }

        [HttpGet("GetAvailableTimeSlots")]
        public IActionResult GetAvailableTimeSlots(string range, int interval = 15)
        {
            try
            {
                var slots = TimeSlotHelper.BreakTimeRange(range, interval);
                return Ok(slots);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
