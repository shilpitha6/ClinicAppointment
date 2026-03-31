using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentProject.Services.AvailabilitySlotsService
{
    public class AvailabilitySlotsService: IAvailabilitySlotsService
    {
        private readonly ClinicAppointmentDatabaseContext _context;

        public AvailabilitySlotsService(ClinicAppointmentDatabaseContext context)
        {
            _context = context;
        }


        public async Task<List<AvailabilitySlotsDTO>> GetSlotsByDoctorIdAsync(int doctor_id)
        {
            var query = from s in _context.AvailabilitySlots
                        where s.doctor_id == doctor_id
                        select new AvailabilitySlotsDTO
                        {
                            slot_id = s.slot_id,
                            doctor_id = s.doctor_id,
                            slot_date = s.slot_date,
                            start_time = s.start_time,
                            end_time = s.end_time
                        };

            return await query.ToListAsync(); 
        }



    }
}
