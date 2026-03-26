using ClinicAppointmentProject.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicAppointmentProject.Services.SpecialtyService
{
    public class SpecialtyService : ISpecialtyService
    {

        private readonly ClinicAppointmentDatabaseContext _context;

        public SpecialtyService(ClinicAppointmentDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            return await _context.Specialty.ToListAsync();
        }

        public async Task<Specialty> GetByIdAsync(int specialty_id)
        {
            return await _context.Specialty.FindAsync(specialty_id);
        }
    }
}
