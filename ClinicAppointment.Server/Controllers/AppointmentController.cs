using ClinicAppointment.Server.Services.AppointmentService;
using ClinicAppointmentProject.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment.Server.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController:ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        [HttpPost]
        public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDTO dto)
        {
            try
            {
                var appointment = await _appointmentService.BookAppointmentAsync(dto);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

    
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO dto)
        {
            try
            {
                var result = await _appointmentService.UpdateStatusAsync(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
