using Constant.Constants;
using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLayer.Implementation
{
    public class DoctorDAL : IDoctorDAL
    {
        private readonly IConfiguration _configuration;
        private readonly string _dbConnectionString;

        public DoctorDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnectionString = _configuration["ConnectionStrings:DB"];
        }
        public async Task<object> GetAllDoctorDetails()
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_get_all_doctor_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var doctorList = new List<object>();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            doctorList.Add(new
                            {
                                email = reader["Email"],
                                id = reader["UserId"],
                                userName = reader["UserName"],
                                firstName = reader["Firstname"],
                                lastName = reader["Lastname"],
                                gender = reader["Gender"],
                                age = reader["age"],
                                phoneNumber = reader["PhoneNumber"],
                                doctorId = reader["DoctorId"],
                                dateOfAssociation = reader["DateOfAssociation"],
                                licenseNumber = reader["LicenseNumber"],
                                qualification = reader["Qualification"],
                                qualificationName = reader["QualificationName"],
                                specialisation = reader["Specialisation"],
                                specialisationName = reader["SpecialisationName"],
                                designation = reader["Designation"],
                                designationName = reader["DesignationName"],
                                experienceYears = reader["ExperienceYears"]
                            });

                        }

                        return doctorList;
                    }
                }
            }
        }

        public async Task<object> GetDoctorDetails(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_get_doctor_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter for userId
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new
                            {
                                email = reader["Email"],
                                id = reader["UserId"],
                                userName = reader["UserName"],
                                firstName = reader["Firstname"],
                                lastName = reader["Lastname"],
                                gender = reader["Gender"],
                                age = reader["age"],
                                phoneNumber = reader["PhoneNumber"],
                                dateOfAssociation = reader["DateOfAssociation"],
                                licenseNumber = reader["LicenseNumber"],
                                qualification = reader["Qualification"],
                                specialisation = reader["Specialisation"],
                                designation = reader["Designation"],
                                experienceYears = reader["ExperienceYears"]
                            };
                        }

                        // Return null if doctor not found
                        return null;
                    }
                }
            }

        }

        public async Task<string> InsertUpdateDoctorDetails(DoctorDetails doctorDetails)
        {
            using(SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("sp_update_doctor_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("UserId", doctorDetails.UserId);
                    cmd.Parameters.AddWithValue("@DateOfAssociation", doctorDetails.DateOfAssociation);
                    cmd.Parameters.AddWithValue("@licenseNumber", doctorDetails.LicenseNumber);
                    cmd.Parameters.AddWithValue("@qualification", doctorDetails.Qualification);
                    cmd.Parameters.AddWithValue("@specialisation", doctorDetails.Specialisation);
                    cmd.Parameters.AddWithValue("@designation", doctorDetails.Designation);
                    cmd.Parameters.AddWithValue("@experienceYears", doctorDetails.ExperienceYears);

                    // IF CREATEBY IS NOT EMPTY THEN ADD PARAMETER
                    if (doctorDetails.CreatedBy != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@createdBy", doctorDetails.CreatedBy);
                    }

                    // IF UPDATEBY IS NOT EMPTY THEN ADD PARAMETER
                    if (doctorDetails.UpdatedBy != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@updatedBy", doctorDetails.UpdatedBy);
                    }

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        return AppConstants.DBResponse.Success;
                    }
                    else
                    {
                        return AppConstants.DBResponse.Failed;
                    }
                };
            };
        }
    }
}
