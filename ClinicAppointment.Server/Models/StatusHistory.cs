using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAppointmentProject.Models
{
    public class StatusHistory
    {
        [Key]
        public int status_id { get; set; }

        public int appointment_id { get; set; }

        public string? previous_status { get; set; }

        public string? updated_status { get; set; }

        public string? changed_by { get; set; }

        public DateTime changed_at { get; set; }

        public string? reason { get; set; }

        [ForeignKey("appointment_id")]
        public Appointment? Appointment { get; set; }


    }
}
