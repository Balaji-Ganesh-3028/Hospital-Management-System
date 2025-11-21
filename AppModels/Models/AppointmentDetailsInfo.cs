using AppModels.Models;

namespace AppModels.ResponseModels
{
    public class AppointmentDetailsInfo: DateTimeStamps
    {
        public int AppointmentId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public int? PurposeOfVisit { get; set; }
        public string? PurposeOfVisitName { get; set; }
        public string? IllnessOrDisease { get; set; }
        public string? ProceduresOrMedication { get; set; }
        public int CurrentStatus { get; set; }
        public string? Status { get; set; }
    }
}
