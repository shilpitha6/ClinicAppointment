namespace ClinicAppointmentProject.DTO
{
    public class PatientDTO
    {
        public int patient_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
    }

    public class RegisterPatientDTO
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public DateOnly dob { get; set; }
        public string? gender { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
    }

    public class LoginDTO
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
