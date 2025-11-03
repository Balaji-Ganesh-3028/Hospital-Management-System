using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

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

        public async Task<string> UserProfileUpdate(UserProfile userProfile)
        {
            try
            {
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

                            userProfileCmd.Parameters.AddWithValue("@userId", userProfile.UserDetails.UserId);
                            userProfileCmd.Parameters.AddWithValue("@firstName", userProfile.UserDetails.FristName);
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
                                contactInfoCmd.Parameters.AddWithValue("@createdBy", userProfile.ContactDetails.CreatedBy);
                            }

                            // IF UPDATEBY IS NOT EMPTY THEN ADD PARAMETER
                            if (userProfile.ContactDetails.UpdatedBy != string.Empty)
                            {
                                contactInfoCmd.Parameters.AddWithValue("@updatedBy", userProfile.ContactDetails.UpdatedBy);
                            }

                            int rowsAffected = await contactInfoCmd.ExecuteNonQueryAsync();
                        }

                        sqlTransaction.Commit();

                        return "Success";

                    } catch (SqlException sqlEx)
                    {
                        sqlTransaction.Rollback();
                        return "Database error: " + sqlEx.Message;
                    }

                }
            } catch (Exception ex) {
                return "Failed: " + ex.Message;
            }
        }

        public async Task<object> GetAllUserDetails()
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("sp_get_all_user_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var userList = new List<object>();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userList.Add(new
                            {
                                userId = reader.GetInt32(0),
                                email = reader.GetString("email"),
                                firstName = reader.GetString("firstname"),
                                lastName = reader.GetString("lastname"),
                                gender = reader.GetInt32(4),
                                phoneNumber = reader.GetString("phoneNumber"),
                                userType = reader.GetString("userType")
                            });
                        }

                        return userList;
                    }

                };
            }
        }

        public async Task<object> UserProfileDetail(int userId)
        {
            if (userId <= 0)
            {
                return new { message = "Invalid user ID." };
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_get_user_profile_details", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var userProfile = new
                                {
                                    userId = reader["UserId"] as int? ?? 0,
                                    email = reader["Email"] as string,
                                    roleId = reader["RoleId"] as int? ?? 0,
                                    roleName = reader["RoleName"] as string,
                                    userName = reader["UserName"] as string,
                                    firstName = reader["FirstName"] as string,
                                    lastName = reader["LastName"] as string,
                                    gender = reader["Gender"] as int? ?? 0,
                                    genderValue = reader["GenderValue"] as string,
                                    age = reader["Age"] as int? ?? 0,
                                    dob = reader["DOB"],
                                    phoneNumber = reader["PhoneNumber"] as string,
                                    doorFloorBuilding = reader["DoorFloorBuilding"] as string,
                                    addressLine1 = reader["AddressLine1"] as string,
                                    addressLine2 = reader["AddressLine2"] as string,
                                    state = reader["State"] as string,
                                    city = reader["City"] as string,
                                    country = reader["Country"] as string,
                                    pincode = reader["Pincode"] as string
                                };

                                return userProfile;
                            }
                            else
                            {
                                return new { message = "User not found." };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log exception here (e.g., using ILogger or custom logging)
                return new { message = "Database error occurred.", error = ex.Message };
            }
            catch (Exception ex)
            {
                // Log exception here
                return new { message = "An unexpected error occurred.", error = ex.Message };
            }
        }

        public async Task<string> DeleteUserProfileDetails(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    await connection.OpenAsync();

                    using(SqlCommand cmd = new SqlCommand("sp_delete_user_details", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader["message"].ToString() == "Success")
                            {
                                return "User record deleted successfully";
                            } else
                            {
                                return "User record not deleted";
                            }
                        }
                    }
                }
                    
            } catch (Exception ex)
            {
                return "Internal server error: " + ex.Message;
            }
        }
    }
}
