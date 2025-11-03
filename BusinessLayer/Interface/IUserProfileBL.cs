using DataAccessLayer.Models;

namespace BusinessLayer.Interface
{
    public interface IUserProfileBL
    {
        Task<string> UserProfileUpdate(UserProfile userProfile);
        Task<object> GetAllUserDetails();
        Task<object> GetUserDetails(int userId);
        Task<string> DeleteUserProfile(int userId);
    }
}
