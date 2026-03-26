using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.Services.PatientService
{
    public interface IPatientService
    {
        Task<Patient> GetByIdAsync(int patientId);
        Task<IEnumerable<AppointmentDTO>> GetPatientAppointmentsAsync(int patientId);

    }
}
