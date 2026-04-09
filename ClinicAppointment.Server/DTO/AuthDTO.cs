namespace ClinicAppointment.Server.DTO
{
    public class LoginDTO
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }

    public class LoginResponseDTO
    {
        public int patient_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
        public string? token { get; set; }       
        public DateTime expires_at { get; set; } 
    }
}
