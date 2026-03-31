using ClinicAppointment.Server.DTO;


namespace ClinicAppointmentProject.Services.SpecialtyService
{
    public interface ISpecialtyService
    {
        Task<List<SpecialtyDTO>> GetAllAsync();


    }
}
