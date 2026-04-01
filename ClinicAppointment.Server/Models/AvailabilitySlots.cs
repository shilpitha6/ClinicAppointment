using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAppointmentProject.Models
{
    public class AvailabilitySlots
    {
        [Key]
        public int slot_id { get; set; }

        
        public int doctor_id { get; set; }

        public DateOnly slot_date { get; set; }

   

        public TimeOnly end_time { get; set; }

        public bool is_booked { get; set; }

        [ForeignKey("doctor_id")]
        public virtual Doctor? Doctor { get; set; }
        public TimeOnly start_time { get;  set; }
    }
}
