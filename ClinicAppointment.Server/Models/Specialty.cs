using System.ComponentModel.DataAnnotations;

namespace ClinicAppointmentProject.Models
{
    public class Specialty
    {
        [Key]
        public int specialty_id { get; set; }

        public string? Name { get; set; }

   

    }
}
