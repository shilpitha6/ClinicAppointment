using ClinicAppointment.Server.DTO;
using ClinicAppointmentProject.Models;

namespace ClinicAppointmentProject.DTO
{
    public class DoctorDTO
    {
        public int doctor_id { get; set; }

        public string? first_name { get; set; }

        public string? last_name { get; set; }

        public int? specialty_id { get; set; }

        public int? slot_id { get; set; }

        public SpecialtyDTO Specialty { get; set; }

        public List<AvailabilitySlotsDTO>? AvailableSlots { get; set; }
        public string? Name { get; internal set; }
        public List<AvailabilitySlotsDTO>? Slots { get; internal set; }
    }
}
