using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;


namespace ClinicAppointmentProject.Services.PatientService
{
    public class PatientService 
    {
        private readonly ClinicAppointmentDatabaseContext _context;

        public PatientService(ClinicAppointmentDatabaseContext context)
        {
            _context = context;
        }


        //public async Task<IEnumerable<PatientDTO>> GetByIdAsync(int patientId)
        //{
        //    return await _context.Patient.FindAsync(patientId);
        //}

        //public async Task<List<AppointmentDTO>> GetPatientAppointmentsAsync(int patient_id)
        //{
        //    return await (
        //        from a in _context.Appointment
        //        join d in _context.Doctor
        //            on a.doctor_id equals d.doctor_id
        //        join s in _context.Specialty
        //            on d.specialty_id equals s.specialty_id
        //        join sl in _context.AvailabilitySlots
        //            on a.slot_id equals sl.slot_id
        //        where a.patient_id == patient_id
        //        select new AppointmentDTO
        //        {
        //            appointment_id = a.appointment_id,
        //            patient_id = a.patient_id,
        //            first_name = d.first_name,
        //            last_name = d.last_name,
        //            Name = s.Name,
        //            slot_date = sl.slot_date,
        //            slot_time = sl.slot_time,
        //            end_time = sl.end_time,
        //            status = a.status
        //        }
        //    ).ToListAsync();
        //}


    }
}
