using ClinicAppointmentProject.DTO;

namespace ClinicAppointmentProject.Services.AvailabilitySlotsService
{
    public interface IAvailabilitySlotsService
    {

        Task<IEnumerable<AvailabilitySlotsDTO>> GetSlotsByDoctorIdAsync(int doctor_id);


    }
}
