namespace AppModels.ResponseModels
{
    public class AppointmentDetailsResponse
    {
        // Appointment info
        public AppointmentDetailsInfo? AppointmentDetails { get; set; }

        // Patient info
        public PatientInfo? PatientInfo { get; set; }

        // Doctor info
        public DoctorInfo? DoctorInfo { get; set; }

    }
}
