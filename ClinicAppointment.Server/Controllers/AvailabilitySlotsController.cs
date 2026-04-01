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


       



    }
}
