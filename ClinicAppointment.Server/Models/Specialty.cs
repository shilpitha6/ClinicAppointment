using System.ComponentModel.DataAnnotations;


namespace ClinicAppointmentProject.Models
{
    public class Specialty
    {
        [Key]

        public int specialty_id { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
            = new List<Doctor>();



    }
}
