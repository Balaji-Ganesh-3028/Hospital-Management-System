using AppModels.Interfaces;
using AppModels.Models;

namespace AppModels.Views
{
    public class PatientInfoView: UserDetails, IPatientInfo
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int PatientId { get; set; }
        public int BloodGroup { get; set; }
        public string BloodGroupName { get; set; }
        public DateOnly DOJ { get; set; }
        public string Allergies { get; set; }
        public string ChronicDiseases { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsuranceNumber { get; set; }
        public string MedicalHistoryNotes { get; set; }
    }
}
