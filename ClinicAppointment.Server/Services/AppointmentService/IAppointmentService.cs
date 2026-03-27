using ClinicAppointmentProject.DTO;

namespace ClinicAppointment.Server.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<AppointmentDTO> BookAppointmentAsync(CreateAppointmentDTO dto);
        Task<string> UpdateStatusAsync(int appointmentId, UpdateStatusDTO dto);

    }
}
