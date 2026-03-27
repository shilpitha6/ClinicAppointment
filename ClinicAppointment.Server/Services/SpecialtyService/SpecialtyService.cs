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

        public async Task<List<Specialty>> GetAllAsync()
        {
            return await _context.Specialty.OrderBy(s=>s.specialty_id).ToListAsync();
        }

        
    }
}
