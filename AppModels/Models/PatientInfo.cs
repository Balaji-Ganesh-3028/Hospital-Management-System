namespace AppModels.ResponseModels
{
    public class PatientInfo
    {
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
    }
}
