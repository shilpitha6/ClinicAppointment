
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
        public async Task<IActionResult> GetAll( int? specialty_id)
        {
            var doctors = await _doctorService.GetDoctorsAsync(specialty_id);
            if (doctors == null || !doctors.Any()) return NotFound();
            return Ok(doctors);
        }

        [HttpGet("{id}/slots")]
        public async Task<IActionResult> GetDoctorAvailableSlots(int id)
        {
            var doctor = await _doctorService.GetDoctorWithAvailableSlotsAsync(id);

            if (doctor == null)
                return NotFound("Doctor not found");

            return Ok(doctor);
        }


    }


}
