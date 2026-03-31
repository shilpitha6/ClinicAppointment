namespace ClinicAppointment.Server.DTO
{
    public class StatusHistoryDTO
    {
        public int status_id { get; set; }

        public string? previous_status { get; set; }

        public string? updated_status { get; set; }

        public string? changed_by { get; set; }

        public DateTime changed_at { get; set; }

        public string? reason { get; set; }


    }
}
