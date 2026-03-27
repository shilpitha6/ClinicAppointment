using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.Services.DoctorService
{
    public interface IDoctorService
    {
       

        Task<List<DoctorDTO>> GetDoctorsAsync(int? specialtyId);

        Task<List<AvailabilitySlotsDTO>> GetDoctorWithAvailableSlotsAsync(int doctorId);
    }
}
