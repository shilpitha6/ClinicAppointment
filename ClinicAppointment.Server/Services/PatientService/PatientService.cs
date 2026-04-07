using ClinicAppointment.Server.DTO;
using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicAppointmentProject.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly ClinicAppointmentDatabaseContext _context;

        public PatientService(ClinicAppointmentDatabaseContext context)
        {
            _context = context;
        }

        public async Task<PatientDTO> RegisterAsync(RegisterPatientDTO dto)
        {
            bool emailExists = await _context.Patient
                .AnyAsync(p => p.email == dto.email);
            if (emailExists)
                throw new Exception("Email is already registered.");

            bool usernameExists = await _context.Patient
                .AnyAsync(p => p.username == dto.username);
            if (usernameExists)
                throw new Exception("Username is already taken.");

            var patient = new Patient
            {
                first_name = dto.first_name,
                last_name = dto.last_name,
                dob = dto.dob,
                gender = dto.gender,
                email = dto.email,
                address = dto.address,
                username = dto.username,
                password = dto.password
            };

            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            return new PatientDTO
            {
                patient_id = patient.patient_id,
                first_name = patient.first_name,
                last_name = patient.last_name,
                email = patient.email,
                username = patient.username
            };
        }

        public async Task<PatientDTO> LoginAsync(string? username, string? password)
        {
            var patient = await _context.Patient
                .FirstOrDefaultAsync(p => p.username == username
                                       && p.password == password);

            if (patient == null)
                throw new Exception("Invalid username or password.");

            

            return new PatientDTO
            {
                patient_id = patient.patient_id,
               
                username = patient.username
            };
        }

        public async Task<PatientDTO> GetByIdAsync(int patientId)
        {
            return await (
                from p in _context.Patient
                where p.patient_id == patientId
                select new PatientDTO
                {
                    patient_id = p.patient_id,
                    first_name = p.first_name,
                    last_name = p.last_name,
                    email = p.email,
                    username = p.username
                }
            ).FirstOrDefaultAsync();
        }

        public async Task<List<PatientAppointmentDTO>> GetPatientAppointmentsAsync(int patientId)
        {
            var appointments = await (
                from a in _context.Appointment
                join d in _context.Doctor on a.doctor_id equals d.doctor_id
                join s in _context.Specialty on d.specialty_id equals s.specialty_id
                join sl in _context.AvailabilitySlots on a.slot_id equals sl.slot_id
                where a.patient_id == patientId
                select new PatientAppointmentDTO
                {
                    appointment_id = a.appointment_id,
                    patient_id = a.patient_id,
                    doctor_id = a.doctor_id,
                    doctor_first_name = d.first_name,
                    doctor_last_name = d.last_name,
                    specialty_name = s.Name,
                    slot_id = sl.slot_id,
                    slot_date = sl.slot_date,
                    start_time = sl.start_time,
                    end_time = sl.end_time,
                    status = a.status
                }
            ).ToListAsync();

            foreach (var appt in appointments)
            {
                appt.status_history = await _context.StatusHistory
                    .Where(h => h.appointment_id == appt.appointment_id)
                    .OrderBy(h => h.changed_at)
                    .ToListAsync();
            }

            return appointments;
        }
    }



}
