using DataAccessLayer.Models;

namespace DataAccessLayer.Interface
{
    public interface IUserProfileDAL
    {
        public Task<string> UserProfileUpdate(UserProfile userProfile);
        public Task<object> GetAllUserDetails(UserDetailsQuery query);
        public Task<object> UserProfileDetail(int userId);
        public Task<string> DeleteUserProfileDetails(int userId);
    }
}
