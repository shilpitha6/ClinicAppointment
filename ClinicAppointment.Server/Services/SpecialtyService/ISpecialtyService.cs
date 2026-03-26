using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.Services.SpecialtyService
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<Specialty>> GetAllAsync();
        Task<Specialty> GetByIdAsync(int specialty_id);

    }
}
