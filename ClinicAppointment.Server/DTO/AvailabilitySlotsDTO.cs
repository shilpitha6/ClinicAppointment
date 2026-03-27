using ClinicAppointment.Server.DTO;

namespace ClinicAppointmentProject.DTO
{
    public class AvailabilitySlotsDTO
    {
        public int slot_id { get; set; }

        public int doctor_id { get; set; }

        public DateOnly slot_date { get; set; }

        public TimeOnly slot_time { get; set; }

        public TimeOnly end_time { get; set; }

        
    }
}
