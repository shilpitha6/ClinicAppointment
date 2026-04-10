using ClinicAppointment.Server.DTO;

namespace ClinicAppointmentProject.DTO

{
    public class CreateAppointmentDTO
    {
        public int slot_id { get; set; }
        public int doctor_id { get; set; }
        public int patient_id { get; set; }
    }

    public class UpdateStatusDTO
    {
        public string? new_status { get; set; }
        public string? changed_by { get; set; }
        public string? reason { get; set; }
    }

    public class AppointmentDTO
    {
        public int appointment_id { get; set; }

        public int slot_id { get; set; }

        public int doctor_id { get; set; }

        public int patient_id { get; set; }

        public string? status { get; set; }

        public DateOnly created_at { get; set; }
       
    }

    

    public class AppointmentDetailsDTO
    {
        public int appointment_id { get; set; }
        public int patient_id { get; set; }
        public int doctor_id { get; set; }
        public string? doctor_first_name { get; set; }
        public string? doctor_last_name { get; set; }
        public string? specialty_name { get; set; }
        public int slot_id { get; set; }
        public string? slot_date { get; set; }
        public string? start_time { get; set; }
        public string? end_time { get; set; }
        public string status { get; set; } = "Pending";
    
    }
}
