namespace DataAccessLayer.Models
{
    public class DoctorDetails
    {
        public int UserId { get; set; }
        public DateOnly DateOfAssociation { get; set; }
        public string LicenseNumber { get; set; }
        public int Qualification { get; set; }
        public int Specialisation { get; set; }
        public int Designation { get; set; }
        public int ExperienceYears { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "SYSTEM";
        public string UpdatedBy { get; set; } = "SYSTEM";
    }
}
