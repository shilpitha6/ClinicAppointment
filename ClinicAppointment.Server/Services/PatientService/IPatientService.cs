using ClinicAppointment.Server.DTO;
using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.Services.PatientService
{
    public interface IPatientService
    {
        Task<PatientDTO> RegisterAsync(RegisterPatientDTO dto);
        Task<LoginResponseDTO> LoginAsync(LoginDTO dto);
        Task<PatientDTO> GetByIdAsync(int patientId);
        Task<List<PatientAppointmentDTO>> GetPatientAppointmentsAsync(int patientId);
    }
}
