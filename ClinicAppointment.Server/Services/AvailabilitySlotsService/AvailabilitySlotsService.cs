using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.Services.AvailabilitySlotsService
{
    public class AvailabilitySlotsService: IAvailabilitySlotsService
    {
        private readonly ClinicAppointmentDatabaseContext _context;

        public AvailabilitySlotsService(ClinicAppointmentDatabaseContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<AvailabilitySlotsDTO>> GetSlotsByDoctorIdAsync(int doctor_id)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<AvailabilitySlotsDTO>> GetSlotsByDoctorIdAsync(int doctor_id)
        //{
        //    return await (
        //        from s in _context.AvailabilitySlots
        //        where s.doctor_id == doctor_id
        //        select new AvailabilitySlotsDTO
        //        {
        //            slot_id = s.slot_id,
        //            doctor_id = s.doctor_id,
        //            slot_date = s.slot_date,
        //            slot_time = s.slot_time,
        //            end_time = s.end_time
        //        }
        //        ).ToListAsync();
        //}



    }
}
