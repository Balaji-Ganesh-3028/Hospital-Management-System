using DataAccessLayer.Models;

namespace BusinessLayer.Interface
{
    public interface IUserProfileBL
    {
        Task<string> UserProfile(UserProfile userProfile);
        Task<string> UserProfileUpdate(UserProfile userProfile);
        Task<object> GetAllUserDetails(string searchTerm, string userType);
        Task<object> GetUserDetails(int userId);
        Task<string> DeleteUserProfile(int userId);
    }
}
