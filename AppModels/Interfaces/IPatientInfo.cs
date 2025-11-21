using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Interfaces
{
    public interface IPatientInfo
    {
        int PatientId { get; set; }
        int BloodGroup { get; set; }
        string BloodGroupName { get; set; }
        DateOnly DOJ { get; set; }
        string Allergies { get; set; }
        string ChronicDiseases { get; set; }
        string EmergencyContactName { get; set; }
        string EmergencyContactNumber { get; set; }
        string InsuranceProvider { get; set; }
        string InsuranceNumber { get; set; }
        string MedicalHistoryNotes { get; set; }
    }
}
