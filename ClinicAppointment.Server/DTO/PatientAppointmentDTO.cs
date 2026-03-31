using ClinicAppointmentProject.Models;

namespace ClinicAppointment.Server.DTO
{
    public class PatientAppointmentDTO
    {
        public int appointment_id { get; set; }
        public int patient_id { get; set; }
        public int doctor_id { get; set; }
        public string? doctor_first_name { get; set; }
        public string? doctor_last_name { get; set; }
        public string? specialty_name { get; set; }
        public int slot_id { get; set; }
        public DateOnly slot_date { get; set; }
        public TimeOnly start_time { get; set; }
        public TimeOnly end_time { get; set; }
        public string? status { get; set; }
        public List<StatusHistory>? status_history { get; set; }
    }
}
