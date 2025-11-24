namespace AppModels.Models
{
    public class ContactDetails
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string doorFloorBuilding { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Pincode { get; set; }
    }
}
