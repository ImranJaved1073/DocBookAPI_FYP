using DocBookAPI.DTOs;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionDTO prescriptionDto)
        {
            var result = await _prescriptionService.CreatePrescription(prescriptionDto);
            if (result != null)
            {
                return CreatedAtAction(nameof(GetPrescription), new { id = result.Id }, result);
            }
            return BadRequest("Failed to create prescription");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescription(int id)
        {
            var result = await _prescriptionService.GetPrescription(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] Prescription prescriptionDto)
        {
            var result = await _prescriptionService.UpdatePrescription(prescriptionDto);
            if (result)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var result = await _prescriptionService.DeletePrescription(id);
            if (result)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("bydoctor/{doctorId}")]
        public async Task<IActionResult> GetPrescriptionsByDoctor(int doctorId)
        {
            var result = await _prescriptionService.GetPrescriptionsByDoctorId(doctorId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("bypatient/{patientId}")]
        public async Task<IActionResult> GetPrescriptionsByPatient(int patientId)
        {
            var result = await _prescriptionService.GetPrescriptionsByPatientId(patientId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("byappointment/{appointmentId}")]
        public async Task<IActionResult> GetPrescriptionsByAppointment(int appointmentId)
        {
            var result = await _prescriptionService.GetPrescriptionsByAppointmentId(appointmentId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("bydoctorandpatient/{doctorId}/{patientId}")]
        public async Task<IActionResult> GetPrescriptionsByDoctorAndPatient(int doctorId, int patientId)
        {
            var result = await _prescriptionService.GetPrescriptionsByDoctorIdAndPatientId(doctorId, patientId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var result = await _prescriptionService.GetPrescriptions();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
