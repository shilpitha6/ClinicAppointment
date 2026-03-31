using ClinicAppointmentProject.Services.DoctorService;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentProject.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

  
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? specialtyId)
        {
            var doctors = await _doctorService.GetDoctorsAsync(specialtyId);
            return Ok(doctors);
        }

    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) 
                return NotFound(new { message = "Doctor not found." });
            return Ok(doctor);
        }

      
        [HttpGet("{id}/slots")]
        public async Task<IActionResult> GetSlots(int id)
        {
            var slots = await _doctorService.GetDoctorWithAvailableSlotsAsync(id);
            if (slots == null || slots.Count == 0)
                return NotFound(new { message = "No available slots for this doctor." });
            return Ok(slots);
        }
    }
}