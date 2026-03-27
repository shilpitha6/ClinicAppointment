using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAppointmentProject.Models
{
    public class AvailabilitySlots
    {
        [Key]
        public int slot_id { get; set; }

        [ForeignKey("doctor_id")]
        public int doctor_id { get; set; }

        public DateOnly slot_date { get; set; }

        public TimeOnly slot_time { get; set; }

        public TimeOnly end_time { get; set; }


        public Doctor? Doctor { get; set; }


    }
}
