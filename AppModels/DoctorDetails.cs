namespace AppModels.Models
{
    public class DoctorDetails: DateTimeStamps
    {
        public int UserId { get; set; }
        public DateOnly DateOfAssociation { get; set; }
        public string LicenseNumber { get; set; }
        public int Qualification { get; set; }
        public int Specialization { get; set; }
        public int Designation { get; set; }
        public int Experience { get; set; }
    }
}
