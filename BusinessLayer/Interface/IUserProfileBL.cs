using AppModels.Models;
using AppModels.RequestModels;

namespace BusinessLayer.Interface
{
    public interface IUserProfileBL
    {
        Task<string> UserProfile(UserProfileRequest userProfile);
        Task<string> UserProfileUpdate(UserProfileRequest userProfile);
        Task<object> GetAllUserDetails(UserDetailsQuery query);
        Task<object> GetUserDetails(int userId);
        Task<string> DeleteUserProfile(int userId);
    }
}
