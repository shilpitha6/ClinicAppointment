using System.ComponentModel.DataAnnotations;

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

        public string? changed_at { get; set; }

        public string? reason { get; set; }


    }
}
