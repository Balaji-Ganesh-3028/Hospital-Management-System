namespace DataAccessLayer.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateOnly DOB { get; set; } 
        public int gender { get; set; }
        public int age { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
