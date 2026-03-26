using System.ComponentModel.DataAnnotations;

namespace ClinicAppointmentProject.Models
{
    public class StatusLookup
    {
        [Key]
        public int status_id { get; set; }
        public string? Name { get; set; }
    }
}
