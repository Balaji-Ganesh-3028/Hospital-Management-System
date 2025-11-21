namespace AppModels.Models
{
    public class PatientDetails: DateTimeStamps
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
    }
}
