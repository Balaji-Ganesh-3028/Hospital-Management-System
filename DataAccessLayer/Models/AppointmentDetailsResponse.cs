using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AppointmentDetailsResponse
    {
        // Appointment info
        public int AppointmentId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public int PurposeOfVisit { get; set; }
        public string? IllnessOrDisease { get; set; }
        public string? ProceduresOrMedication { get; set; }
        public int CurrentStatus { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Patient info
        public int PatientId { get; set; }
        public string? PatientFirstName { get; set; }
        public string? PatientLastName { get; set; }
        public int? PatientAge { get; set; }
        public int? PatientGender { get; set; }
        public string? PatientPhoneNumber { get; set; }
        public string? PatientAddressLine1 { get; set; }
        public string? PatientAddressLine2 { get; set; }
        public string? PatientCity { get; set; }
        public string? PatientState { get; set; }
        public string? PatientCountry { get; set; }
        public string? PatientBloodGroup { get; set; }


        // Doctor info
        public int DoctorId { get; set; }
        public string? DoctorFirstName { get; set; }
        public string? DoctorLastName { get; set; }
        public string? Specialization { get; set; }

        public string? Designation {  get; set; }
        public int? Experience { get; set; }
        public string? DoctorPhoneNumber { get; set; }
        public string? DoctorAddressLine1 { get; set; }
        public string? DoctorCity { get; set; }
        public string? DoctorState { get; set; }
        public string? DoctorCountry { get; set; }

    }
}
