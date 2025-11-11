using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int PurposeOfVisit { get; set; }
        public string? IllnessOrDisease { get; set; }
        public string? ProceduresOrMedication { get; set; }
        public int CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "SYSTEM";
        public string UpdatedBy { get; set; } = "SYSTEM";

    }
}
