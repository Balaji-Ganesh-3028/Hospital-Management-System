namespace AppModels.Models
{
    public class Appointment: DateTimeStamps
    {
        public int Id { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int PurposeOfVisit { get; set; }
        public string? IllnessOrDisease { get; set; }
        public string? ProceduresOrMedication { get; set; }
        public int CurrentStatus { get; set; }
    }
}
