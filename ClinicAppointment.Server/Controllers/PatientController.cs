using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Services.PatientService;

using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterPatientDTO dto)
        {
            try
            {
                var result = await _patientService.RegisterAsync(dto);
                return CreatedAtAction(nameof(Register),
                    new { id = result.patient_id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var result = await _patientService.LoginAsync(dto.username, dto.password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound(new { message = "Patient not found." });
            return Ok(patient);
        }

        [HttpGet("{id}/appointments")]
        public async Task<IActionResult> GetAppointments(int id)
        {
            var appointments = await _patientService.GetPatientAppointmentsAsync(id);
            return Ok(appointments);
        }

    }
}
