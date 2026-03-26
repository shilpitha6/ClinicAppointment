using ClinicAppointmentProject.Services.SpecialtyService;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentProject.Controllers
{
    [ApiController]
    [Route("api/specialties")]
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyService _specialty;

        public SpecialtyController(ISpecialtyService specialty)
        {
            _specialty = specialty;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var specialties = await _specialty.GetAllAsync();
            return Ok(specialties);
        }

        [HttpGet("{specialty_id}")]
        public async Task<IActionResult> GetById(int specialty_id)
        {
            var specialty = await _specialty.GetByIdAsync(specialty_id);
            if (specialty == null) return NotFound();
            return Ok(specialty);
        }



    }
}
