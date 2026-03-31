using ClinicAppointment.Server.Services.AppointmentService;
using ClinicAppointmentProject.DTO;

using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentProject.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Book([FromBody] CreateAppointmentDTO dto)
        {
            try
            {
                var result = await _appointmentService.BookAppointmentAsync(dto);
                return CreatedAtAction(nameof(Book),
                    new { id = result.appointment_id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO dto)
        {
            try
            {
                var result = await _appointmentService.UpdateStatusAsync(id, dto);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

      
    }
}