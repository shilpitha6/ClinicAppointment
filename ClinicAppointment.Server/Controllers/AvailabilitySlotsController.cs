using ClinicAppointmentProject.Services.AvailabilitySlotsService;
using Microsoft.AspNetCore.Mvc;


namespace ClinicAppointmentProject.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class AvailabilitySlotsController
    {
        private readonly IAvailabilitySlotsService _slotService;

        public AvailabilitySlotsController(IAvailabilitySlotsService slotService)
        {
            _slotService = slotService;
        }


        //[HttpGet("{doctor_id}/slots")]
        //public async Task<IActionResult> GetSlotsByDoctorIdAsync(int doctor_id)
        //{
        //    var slots =await _slotService.GetSlotsByDoctorIdAsync(doctor_id);

        //    if (slots == null || !slots.Any())
        //    {
        //        return NotFound("No slots found for doctor {doctor_id}");
        //    }
        //    return Ok(slots);
        //}



    }
}
