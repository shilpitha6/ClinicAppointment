using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //[ForeignKey("doctor_id")]
        public virtual  Doctor? Doctor { get; set; }

        //[ForeignKey("patient_id")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("slot_id")]
        public AvailabilitySlots ? AvailabilitySlots { get; set; }

        public virtual ICollection<StatusHistory> StatusHistories { get; set; }
            = new List<StatusHistory>();

    }
}
