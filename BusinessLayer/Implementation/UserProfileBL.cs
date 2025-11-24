
using AppModels.Models;
using AppModels.RequestModels;
using BusinessLayer.Interface;
using DataAccessLayer.Interface;

namespace BusinessLayer.Implementation
{
    public class UserProfileBL: IUserProfileBL
    {
        private readonly IUserProfileDAL _userProfileDal;
        public UserProfileBL(IUserProfileDAL userProfileDAL) 
        {
            _userProfileDal = userProfileDAL;
        }
        public async Task<string> UserProfileUpdate(UserProfileRequest userProfile)
        {
            // CALL DATA ACCESS LAYER TO UPDATE USER PROFILE
            return await _userProfileDal.UserProfileUpdate(userProfile);
        }

        public async Task<string> UserProfile(UserProfileRequest userProfile)
        {
            // CALL DATA ACCESS LAYER TO INSERT USER PROFILE
            return await _userProfileDal.UserProfileUpdate(userProfile);
        }


        public async Task<object> GetAllUserDetails(UserDetailsQuery query)
        {
            // CALL DATA ACCESS LAYER TO GET ALL USER DETAILS
            return await _userProfileDal.GetAllUserDetails(query);
        }

        public async Task<object> GetUserDetails(int userId)
        {
            // CALL DATA ACCESS LAYER TO GET USER DETAILS BY USER ID
            return await _userProfileDal.UserProfileDetail(userId);
        }

        public async Task<string> DeleteUserProfile(int userId)
        {
            // CALL DATA ACCESS LAYER TO DELETE USER PROFILE BY USER ID
            return await _userProfileDal.DeleteUserProfileDetails(userId);
        }
    }
}
