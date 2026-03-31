using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetDoctorsAsync(int? specialtyId);
        Task<DoctorDTO> GetDoctorByIdAsync(int doctorId);
        Task<List<AvailabilitySlotsDTO>> GetDoctorWithAvailableSlotsAsync(int doctorId);


    }
}
