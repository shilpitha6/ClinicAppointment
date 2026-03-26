using ClinicAppointment.Server.DTO;
using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClinicAppointmentProject.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {

        private readonly ClinicAppointmentDatabaseContext _context;

        public DoctorService(ClinicAppointmentDatabaseContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<DoctorDTO>> GetAllAsync()
        {
            return await (
                from d in _context.Doctor
                join s in _context.Specialty on d.specialty_id equals s.specialty_id
                select new DoctorDTO
                {
                    doctor_id = d.doctor_id,
                    first_name = d.first_name,
                    last_name = d.last_name,
                    specialty_id = d.specialty_id

                }
                ).ToListAsync();
        }

        public async Task<DoctorDTO> GetByIdAsync(int doctor_id)
        {
            return await (
                from d in _context.Doctor
                join s in _context.Specialty on d.specialty_id equals s.specialty_id
                where d.doctor_id == doctor_id
                select new DoctorDTO
                {
                    doctor_id = d.doctor_id,
                    first_name = d.first_name,
                    last_name = d.last_name,

                    Specialty = new SpecialtyDTO
                    {

                        Name = s.Name

                    }

                }
                ).FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<DoctorDTO>> GetSlotsByDoctorIdAsync(int specialty_id)
        {
            return await (
                from d in _context.Doctor
                join s in _context.Specialty on d.specialty_id equals s.specialty_id

                select new DoctorDTO
                {

                    doctor_id = d.doctor_id,
                    first_name = d.first_name,
                    last_name = d.last_name,
                    Specialty = new SpecialtyDTO
                    {
                        specialty_id = s.specialty_id,
                        Name = s.Name
                    }

                }
                ).ToListAsync();

        }

    }
}
