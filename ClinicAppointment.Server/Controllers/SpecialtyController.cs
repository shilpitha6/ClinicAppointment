using ClinicAppointmentProject.Services.SpecialtyService;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentProject.Controllers
{
    [ApiController]
    [Route("api/specialties")]
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var specialties = await _specialtyService.GetAllAsync();
            return Ok(specialties);
        }


    }
}
