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

      



    }
}
