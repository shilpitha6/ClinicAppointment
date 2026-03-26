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

        public int slot_id { get; set; }


        [ForeignKey("specialty_id")]
        public Specialty? Specialty { get; set; }

        [ForeignKey("slot_id")]
        public AvailabilitySlots? AvailabilitySlots { get; set; }

    }
}
