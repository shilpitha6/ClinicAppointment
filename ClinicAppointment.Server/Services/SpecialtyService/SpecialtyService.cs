using ClinicAppointment.Server.DTO;
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

        public async Task<List<SpecialtyDTO>> GetAllAsync()
        {
            return await (
                from s in _context.Specialty
                orderby s.specialty_id
                select new SpecialtyDTO
                {
                    specialty_id = s.specialty_id,
                    Name = s.Name
                }
            ).ToListAsync();
        }

    }
}
