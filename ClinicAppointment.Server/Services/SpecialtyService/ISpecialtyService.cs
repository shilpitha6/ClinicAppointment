using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.Services.SpecialtyService
{
    public interface ISpecialtyService
    {
        Task<List<Specialty>> GetAllAsync();
        

    }
}
