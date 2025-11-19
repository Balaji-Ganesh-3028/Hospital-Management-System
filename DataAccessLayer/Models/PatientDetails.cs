namespace DataAccessLayer.Models
{
    public class PatientDetails
    {
        public int UserId { get; set; }
        public DateOnly? DOJ { get; set; }
        public int? BloodGroup { get; set; }
        public string? Allergies { get; set; }
        public string? ChronicDiseases { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? InsuranceNumber { get; set; }    
        public string? MedicalHistoryNotes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "SYSTEM";
        public string UpdatedBy { get; set; } = "SYSTEM";
    }
}
