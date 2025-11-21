namespace AppModels.Models
{
    public class DateTimeStamps
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "SYSTEM";
        public string UpdatedBy { get; set; } = "SYSTEM";
    }
}
