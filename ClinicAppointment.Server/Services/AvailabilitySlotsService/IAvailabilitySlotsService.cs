using ClinicAppointmentProject.DTO;

namespace ClinicAppointmentProject.Services.AvailabilitySlotsService
{
    public interface IAvailabilitySlotsService
    {

        Task<List<AvailabilitySlotsDTO>> GetSlotsByDoctorIdAsync(int doctor_id);


    }
}
