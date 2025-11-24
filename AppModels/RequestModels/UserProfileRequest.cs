using AppModels.Models;

namespace AppModels.RequestModels
{
    public class UserProfileRequest
    {
        public UserDetails UserDetails { get; set; }
        public ContactDetails ContactDetails { get; set; }
    }
}
