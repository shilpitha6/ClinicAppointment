using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.Services.DoctorService
{
    public interface IDoctorService
    {


        Task<DoctorDTO> GetByIdAsync(int doctor_id);

        Task<IEnumerable<DoctorDTO>> GetSlotsByDoctorIdAsync(int specialty_id);
    }
}
