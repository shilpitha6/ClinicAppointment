using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAppointmentProject.Models
{
    public class Doctor
    {
        [Key]
        public int doctor_id { get; set; }

        public string? first_name { get; set; }

        public string? last_name { get; set; }

        public int specialty_id { get; set; }

        [ForeignKey("specialty_id")]
        public virtual Specialty? Specialty { get; set; }

        public virtual ICollection<AvailabilitySlots> AvailabilitySlots { get; set; }
           = new List<AvailabilitySlots>();

        public virtual ICollection<Appointment> Appointments { get; set; }
            = new List<Appointment>();

    }
}
