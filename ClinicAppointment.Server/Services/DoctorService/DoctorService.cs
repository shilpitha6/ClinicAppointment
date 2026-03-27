using ClinicAppointment.Server.DTO;
using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace ClinicAppointmentProject.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {

        private readonly ClinicAppointmentDatabaseContext _context;

        public DoctorService(ClinicAppointmentDatabaseContext context)
        {
            _context = context;
        }


        public async Task<List<DoctorDTO>> GetDoctorsAsync(int? specialtyId)
        {
            var query = from d in _context.Doctor
                        join s in _context.Specialty on d.specialty_id equals s.specialty_id
                       
                        select new DoctorDTO
                        {
                            doctor_id = d.doctor_id,
                            first_name = d.first_name,
                            last_name = d.last_name,
                            specialty_id = d.specialty_id,
                            Specialty = new SpecialtyDTO
                            {
                                specialty_id = s.specialty_id,
                                Name = s.Name
                            }
                        };
            if (specialtyId!= null)
            {
                query = query.Where(d => d.specialty_id == specialtyId);
            }

            return await query.ToListAsync();

        }

        public async Task<List<AvailabilitySlotsDTO>> GetDoctorWithAvailableSlotsAsync(int doctorId)
        {
            

            var bookedSlotIds = _context.Appointment
                .Where(a => a.doctor_id == doctorId && a.status != "Cancelled")
                .Select(a => a.slot_id);

           return await _context.AvailabilitySlots
                .Where(s => s.doctor_id == doctorId && !bookedSlotIds.Contains(s.slot_id))
                .OrderBy(s => s.slot_date)
                .ThenBy(s => s.slot_time)
                .Select(s => new AvailabilitySlotsDTO
                {
                    slot_id = s.slot_id,
                    doctor_id = s.doctor_id,
                    slot_date = s.slot_date,
                    slot_time = s.slot_time,
                    end_time = s.end_time
                }).ToListAsync();

        
        }
    }
}


