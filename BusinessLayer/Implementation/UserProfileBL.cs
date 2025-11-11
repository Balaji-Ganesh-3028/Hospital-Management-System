
using BusinessLayer.Interface;
using DataAccessLayer.Interface;
using DataAccessLayer.Models;

namespace BusinessLayer.Implementation
{
    public class UserProfileBL: IUserProfileBL
    {
        private readonly IUserProfileDAL _userProfileDal;
        public UserProfileBL(IUserProfileDAL userProfileDAL) 
        {
            _userProfileDal = userProfileDAL;
        }
        public async Task<string> UserProfileUpdate(UserProfile userProfile)
        {
            return await _userProfileDal.UserProfileUpdate(userProfile);
        }

        public async Task<string> UserProfile(UserProfile userProfile)
        {
            return await _userProfileDal.UserProfileUpdate(userProfile);
        }


        public async Task<object> GetAllUserDetails(string? searchTerm, string? userType)
        {
            return await _userProfileDal.GetAllUserDetails(searchTerm, userType);
        }

        public async Task<object> GetUserDetails(int userId)
        {
            return await _userProfileDal.UserProfileDetail(userId);
        }

        public async Task<string> DeleteUserProfile(int userId)
        {
            return await _userProfileDal.DeleteUserProfileDetails(userId);
        }
    }
}
