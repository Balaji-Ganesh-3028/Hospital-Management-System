using AppModels.Models;
using AppModels.Views;
using Constant.Constants;
using DataAccessLayer.Interface;
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
                            doctorList.Add(new DoctorInfoView
                            {
                                Email = reader["Email"] as string ?? "",
                                Id = reader["UserId"] as int? ?? 0,
                                UserName = reader["UserName"] as string ?? "",
                                FirstName = reader["Firstname"] as string ?? "",
                                LastName = reader["Lastname"] as string ?? "",
                                Gender = reader["Gender"] as int? ?? 0,
                                Age = reader["age"] as int? ?? 0,
                                PhoneNumber = reader["PhoneNumber"] as string ?? "",
                                DoctorId = reader["DoctorId"] as int? ?? 0,
                                DateOfAssociation = DateOnly.FromDateTime((DateTime)reader["DateOfAssociation"]),
                                LicenseNumber = reader["LicenseNumber"] as string ?? "",
                                Qualification = (int)reader["Qualification"],
                                QualificationName = reader["QualificationName"] as string ?? "",
                                Specialization = reader["Specialisation"] as int? ?? 0,
                                SpecializationName = reader["SpecialisationName"] as string ?? "",
                                Designation = reader["Designation"] as int? ?? 0,
                                DesignationName = reader["DesignationName"] as string ?? "",
                                Experience = reader["ExperienceYears"] as int? ?? 0
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
                        if (!await reader.ReadAsync())
                            return null;
                        
                        return new DoctorInfoView
                        {
                            Email = (string)reader["Email"],
                            Id = reader["UserId"] as int? ?? 0,
                            UserId = reader["UserId"] as int? ?? 0,
                            UserName = reader["UserName"] as string ?? "",
                            FirstName = reader["Firstname"] as string ?? "",
                            LastName = reader["Lastname"] as string ?? "",
                            Gender = reader["Gender"] as int? ?? 0,
                            Age = reader["Age"] as int? ?? 0,
                            PhoneNumber = reader["PhoneNumber"] as string ?? "",
                            DoctorId = reader["DoctorId"] as int? ?? 0,
                            DateOfAssociation = DateOnly.FromDateTime((DateTime)reader["DateOfAssociation"]),
                            LicenseNumber = reader["LicenseNumber"] as string ?? "",
                            Qualification = (int)reader["Qualification"],
                            QualificationName = reader["QualificationName"] as string ?? "",
                            Specialization = (int)reader["Specialisation"],
                            SpecializationName = reader["SpecialisationName"] as string ?? "",
                            Designation = reader["Designation"] as int? ?? 0,
                            DesignationName = reader["DesignationName"] as string ?? "",
                            Experience = reader["ExperienceYears"] as int? ?? 0
                        };
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
                    cmd.Parameters.AddWithValue("@dateOfAssociation", doctorDetails.DateOfAssociation);
                    cmd.Parameters.AddWithValue("@licenseNumber", doctorDetails.LicenseNumber);
                    cmd.Parameters.AddWithValue("@qualification", doctorDetails.Qualification);
                    cmd.Parameters.AddWithValue("@specialisation", doctorDetails.Specialization);
                    cmd.Parameters.AddWithValue("@designation", doctorDetails.Designation);
                    cmd.Parameters.AddWithValue("@experienceYears", doctorDetails.Experience);

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
