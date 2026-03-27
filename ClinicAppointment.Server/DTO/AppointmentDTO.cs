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
}
