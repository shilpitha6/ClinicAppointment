using System.ComponentModel.DataAnnotations;

namespace ClinicAppointmentProject.Models
{
    public class Appointment
    {
        [Key]
        public int appointment_id { get; set; }

        public int slot_id { get; set; }

        public int doctor_id { get; set; }

        public int patient_id { get; set; }

        public string? status { get; set; }

        public DateOnly created_at { get; set; }


    }
}
