using ClinicAppointmentProject.Services.PatientService;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetById(int patient_id)
        {
            var patient = await _patientService.GetByIdAsync(patient_id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpGet("{id}/appointments")]
        public async Task<IActionResult> GetPatientAppointments(int patient_id)
        {
            
            var patient = await _patientService.GetByIdAsync(patient_id);
            if (patient == null) return NotFound($"Patient with id {patient_id} not found");

            var appointments = await _patientService.GetPatientAppointmentsAsync(patient_id);
            return Ok(appointments);
        }

    }
}
