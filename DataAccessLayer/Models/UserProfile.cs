using DataAccessLayer.models;

namespace DataAccessLayer.Models
{
    public class UserProfile
    {
        public UserDetails UserDetails { get; set; }
        public ContactDetails ContactDetails { get; set; }
    }
}
