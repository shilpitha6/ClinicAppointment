
using ClinicAppointmentProject.Models;
using ClinicAppointmentProject.Services.DoctorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ClinicAppointmentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();
            return Ok(doctors);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int doctor_id)
        {
            var doctors = await _doctorService.GetByIdAsync(doctor_id);
            if (doctors == null) return NotFound();
            return Ok(doctors);
        }

        [HttpGet("{doctor_id}")]
        public async Task<IActionResult> GetSlotsByDoctorId(int doctor_id)
        {
            var doctor = await _doctorService.GetSlotsByDoctorIdAsync(doctor_id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }


    }


}
