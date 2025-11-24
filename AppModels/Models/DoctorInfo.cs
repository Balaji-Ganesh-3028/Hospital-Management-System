namespace AppModels.ResponseModels
{
    public class DoctorInfo
    {
        public int DoctorId { get; set; }
        public string? DoctorFirstName { get; set; }
        public string? DoctorLastName { get; set; }
        public string? Specialization { get; set; }
        public string? Designation { get; set; }
        public int? Experience { get; set; }
        public string? DoctorPhoneNumber { get; set; }
        public string? DoctorAddressLine1 { get; set; }
        public string? DoctorCity { get; set; }
        public string? DoctorState { get; set; }
        public string? DoctorCountry { get; set; }
    }
}
