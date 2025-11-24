using AppModels.Models;
using AppModels.RequestModels;
using AppModels.Views;
using Constant.Constants;
using DataAccessLayer.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLayer.Implementation
{
    public class UserProfileDAL: IUserProfileDAL
    {
        private readonly IConfiguration _connectionString;
        private readonly string _dbConnectionString;

        public UserProfileDAL(IConfiguration configuration)
        {
            _connectionString = configuration;
            _dbConnectionString = _connectionString["ConnectionStrings:DB"];
        }

        public async Task<string> UserProfileUpdate(UserProfileRequest userProfile)
        {
            var userId = 0;

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN THE CONNECTION
                await connection.OpenAsync();

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    // INSERT USER PROFILE
                    using (SqlCommand userProfileCmd = new SqlCommand("sp_update_user_profile", connection, sqlTransaction))
                    {
                        userProfileCmd.CommandType = CommandType.StoredProcedure;

                        // ADD PARAMETERS
                        userProfileCmd.Parameters.AddWithValue("@userId", userProfile.UserDetails.UserId);
                        userProfileCmd.Parameters.AddWithValue("@firstName", userProfile.UserDetails.FirstName);
                        userProfileCmd.Parameters.AddWithValue("@lastName", userProfile.UserDetails.LastName);
                        userProfileCmd.Parameters.AddWithValue("@gender", userProfile.UserDetails.Gender);
                        userProfileCmd.Parameters.AddWithValue("@age", userProfile.UserDetails.Age);
                        userProfileCmd.Parameters.AddWithValue("@DOB", userProfile.UserDetails.DOB);

                        // IF CREATEBY IS NOT EMPTY THEN ADD PARAMETER
                        if(userProfile.UserDetails.CreatedBy != string.Empty)
                        {
                            userProfileCmd.Parameters.AddWithValue("@createdBy", userProfile.UserDetails.CreatedBy);
                        }

                        // IF UPDATEBY IS NOT EMPTY THEN ADD PARAMETER
                        if(userProfile.UserDetails.UpdatedBy != string.Empty)
                        {
                            userProfileCmd.Parameters.AddWithValue("@updatedBy", userProfile.UserDetails.UpdatedBy);
                        }

                        int rowsAffected = await userProfileCmd.ExecuteNonQueryAsync();

                    }

                    // INSERT CONTACT INFO
                    using (SqlCommand contactInfoCmd = new SqlCommand("sp_update_user_contact_details", connection, sqlTransaction))
                    {
                        contactInfoCmd.CommandType = CommandType.StoredProcedure;

                        // ADD PARAMETERS
                        contactInfoCmd.Parameters.AddWithValue("@userId", userProfile.UserDetails.UserId);
                        contactInfoCmd.Parameters.AddWithValue("@phoneNumber", userProfile.ContactDetails.PhoneNumber);
                        contactInfoCmd.Parameters.AddWithValue("@doorFloorBuilding", userProfile.ContactDetails.doorFloorBuilding);
                        contactInfoCmd.Parameters.AddWithValue("@addressLine1", userProfile.ContactDetails.AddressLine1);
                        contactInfoCmd.Parameters.AddWithValue("@addressLine2", userProfile.ContactDetails.AddressLine2);
                        contactInfoCmd.Parameters.AddWithValue("@country", userProfile.ContactDetails.Country);
                        contactInfoCmd.Parameters.AddWithValue("@city", userProfile.ContactDetails.City);
                        contactInfoCmd.Parameters.AddWithValue("@state", userProfile.ContactDetails.State);
                        contactInfoCmd.Parameters.AddWithValue("@pincode", userProfile.ContactDetails.Pincode);

                        // IF CREATEBY IS NOT EMPTY THEN ADD PARAMETER
                        if (userProfile.UserDetails.CreatedBy != string.Empty)
                        {
                            contactInfoCmd.Parameters.AddWithValue("@createdBy", userProfile.UserDetails.CreatedBy);
                        }

                        // IF UPDATEBY IS NOT EMPTY THEN ADD PARAMETER
                        if (userProfile.UserDetails.UpdatedBy != string.Empty)
                        {
                            contactInfoCmd.Parameters.AddWithValue("@updatedBy", userProfile.UserDetails.UpdatedBy);
                        }


                        int rowsAffected = await contactInfoCmd.ExecuteNonQueryAsync();
                    }

                    sqlTransaction.Commit();

                    // RETURN SUCCESS MESSAGE
                    return AppConstants.DBResponse.Success;

                } catch (SqlException sqlEx)
                {
                    sqlTransaction.Rollback();
                    return AppConstants.ResponseMessages.DatabaseError + sqlEx.Message;
                }

            }
        }

        public async Task<object> GetAllUserDetails(UserDetailsQuery query)
        {
            // CONNECT TO DATABASE
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                // EXECUTE STORE PROCEDURE
                using (SqlCommand cmd = new SqlCommand("sp_get_all_user_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ADD PARAMETERS TO COMMAND
                    cmd.Parameters.AddWithValue("@searchTerm", query.SearchTerm);
                    cmd.Parameters.AddWithValue("@UserTypeFilter", query.UserType);
                    cmd.Parameters.AddWithValue("@pageSize", query.PageSize);
                    cmd.Parameters.AddWithValue("@pageNumber", query.PageNumber);
                    cmd.Parameters.AddWithValue("@SortByOrder", query.SortByOrder);
                    cmd.Parameters.AddWithValue("@SortByColumn", query.SortByColumn);

                    var userList = new List<object>();

                    // EXECUTE READER
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        // READ DATA
                        while (await reader.ReadAsync())
                        {
                            // ADD USER DETAIL TO LIST
                            userList.Add(new UserDetailInfoView
                            {
                                UserId = reader.GetInt32(0),
                                Email = reader.GetString("email"),
                                FirstName = reader.GetString("firstname"),
                                LastName = reader.GetString("lastname"),
                                Gender = reader.GetInt32(4),
                                PhoneNumber = reader.GetString("phoneNumber"),
                                UserType = reader.GetString("userType"),
                                Total = reader["TotalCount"] as int? ?? 0
                            });
                        }

                        // RETURN USER LIST
                        return userList;
                    }

                };
            }
        }

        public async Task<object> UserProfileDetail(int userId)
        {
            // VALIDATE USER ID AND RETURN ERROR IF INVALID
            if (userId <= 0)
            {
                return new { message = AppConstants.ResponseMessages.UserIdRequired };
            }

            // CONNECT TO DATABASE
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                // EXECUTE STORE PROCEDURE
                using (SqlCommand cmd = new SqlCommand("sp_get_user_profile_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ADD PARAMETERS TO COMMAND
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    // EXECUTE READER
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        // READ DATA
                        if (await reader.ReadAsync())
                        {
                            // MAP DATA TO OBJECT AND RETURN
                            var userProfile = new UserDetailInfoView
                            {
                                UserId = reader["UserId"] as int? ?? 0,
                                Email = reader["Email"] as string,
                                RoleId = reader["RoleId"] as int? ?? 0,
                                RoleName = reader["RoleName"] as string,
                                UserName = reader["UserName"] as string,
                                FirstName = reader["FirstName"] as string,
                                LastName = reader["LastName"] as string,
                                Gender = reader["Gender"] as int? ?? 0,
                                GenderValue = reader["GenderValue"] as string,
                                Age = reader["Age"] as int? ?? 0,
                                DOB = (DateTime)reader["DOB"],
                                PhoneNumber = reader["PhoneNumber"] as string,
                                DoorFloorBuilding = reader["DoorFloorBuilding"] as string,
                                AddressLine1 = reader["AddressLine1"] as string,
                                AddressLine2 = reader["AddressLine2"] as string,
                                State = reader["State"] as string,
                                City = reader["City"] as string,
                                Country = reader["Country"] as string,
                                Pincode = reader["Pincode"] as string
                            };

                            // RETURN USER PROFILE
                            return userProfile;
                        }
                        else
                        {
                            return AppConstants.ResponseMessages.UserNotFound;
                        }
                    }
                }
            }
        }

        public async Task<string> DeleteUserProfileDetails(int userId)
        {
            // CONNECT TO DATABASE
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                // EXECUTE STORE PROCEDURE
                using(SqlCommand cmd = new SqlCommand("sp_delete_user_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    // ADD PARAMETERS TO COMMAND
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    // EXECUTE READER
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // READ DATA & RETURN RESPONSE
                        if (reader.Read() && reader["Message"].ToString() == AppConstants.DBResponse.Success)
                        {
                            return AppConstants.DBResponse.Success;
                        } else
                        {
                            return AppConstants.ResponseMessages.UserDeletionFfailed;
                        }
                    }
                }
            }
        }
    }
}
