using AppModels.Models;
using AppModels.Views;
using Constant.Constants;
using DataAccessLayer.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLayer.Implementation
{
    public class PatientDAL: IPatientDAL
    {
        private readonly IConfiguration _configuration;
        private readonly string _dbConnectionString;

        public PatientDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnectionString = _configuration["ConnectionStrings:DB"];
        }
        public async Task<string> InsertUpdatePatientDetails(PatientDetails patientDetails)
        {
            using(SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_update_patient_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //ADD PARAMETERS
                    cmd.Parameters.AddWithValue("@UserId", patientDetails.UserId);
                    cmd.Parameters.AddWithValue("@DOJ", patientDetails.DOJ);
                    cmd.Parameters.AddWithValue("@bloodGroup", patientDetails.BloodGroup);
                    cmd.Parameters.AddWithValue("@allergies", patientDetails.Allergies);
                    cmd.Parameters.AddWithValue("@chronicDiseases", patientDetails.ChronicDiseases);
                    
                    // Emegency Contact Details
                    cmd.Parameters.AddWithValue("@emergencyContactName", patientDetails.EmergencyContactName);
                    cmd.Parameters.AddWithValue("@emergencyContactNumber", patientDetails.EmergencyContactNumber);

                    // Insurance details
                    cmd.Parameters.AddWithValue("@insuranceProvider", patientDetails.InsuranceProvider);
                    cmd.Parameters.AddWithValue("@insuranceNumber", patientDetails.InsuranceNumber);

                    cmd.Parameters.AddWithValue("@MedicalHistoryNotes", patientDetails.MedicalHistoryNotes);

                    // IF CREATEBY IS NOT EMPTY THEN ADD PARAMETER
                    if (patientDetails.CreatedBy != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@createdBy", patientDetails.CreatedBy);
                    }

                    // IF UPDATEBY IS NOT EMPTY THEN ADD PARAMETER
                    if (patientDetails.UpdatedBy != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@updatedBy", patientDetails.UpdatedBy);
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

        public async Task<object> GetAllPatientDetails()
        {
            using(SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                //OPEN CONNECTION
                await connection.OpenAsync();

                using(SqlCommand cmd = new SqlCommand("sp_get_all_patient_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var patientList = new List<object>();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            patientList.Add(new PatientInfoView
                            {
                                Email = reader["Email"] as string ?? "",
                                Id = reader["UserId"] as int? ?? 0,
                                UserName = reader["UserName"] as string ?? "",
                                FirstName = reader["Firstname"] as string ?? "",
                                LastName = reader["Lastname"] as string ?? "",
                                Gender = reader["Gender"] as int? ?? 0,
                                Age = reader["age"] as int? ?? 0,
                                PhoneNumber = reader["PhoneNumber"] as string ?? "",
                                BloodGroup = reader["BloodGroup"] as int? ?? 0,
                                BloodGroupName = reader["BloodGroupName"] as string ?? "",
                                PatientId = reader["PatientId"] as int? ?? 0,
                                DOJ = DateOnly.FromDateTime((DateTime)reader["DOJ"]),
                                Allergies = reader["Allergies"] as string ?? "",
                                ChronicDiseases = reader["ChronicDiseases"] as string ?? "",
                                EmergencyContactName = reader["EmergencyContactName"] as string ?? "",
                                EmergencyContactNumber = reader["EmergencyContactNumber"] as string ?? "",
                                InsuranceProvider = reader["InsuranceProvider"] as string ?? "",
                                InsuranceNumber = reader["InsuranceNumber"] as string ?? "",
                                MedicalHistoryNotes = reader["MedicalHistoryNotes"] as string ?? ""
                            });
                        }

                        return patientList;
                    }
                }
            }
        }

        public async Task<object?> GetPatientDetails(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_get_patient_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter for userId
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new PatientInfoView
                            {
                                Email = reader["Email"] as string ?? "",
                                Id = reader["UserId"] as int? ?? 0,
                                UserName = reader["UserName"] as string ?? "",
                                FirstName = reader["Firstname"] as string ?? "",
                                LastName = reader["Lastname"] as string ?? "",
                                Gender = reader["Gender"] as int? ?? 0,
                                Age = reader["age"] as int? ?? 0,
                                PhoneNumber = reader["PhoneNumber"] as string ?? "",
                                BloodGroup = reader["BloodGroup"] as int? ?? 0,
                                BloodGroupName = reader["BloodGroupName"] as string ?? "",
                                PatientId = reader["PatientId"] as int? ?? 0,
                                DOJ = DateOnly.FromDateTime((DateTime)reader["DOJ"]),
                                Allergies = reader["Allergies"] as string ?? "",
                                ChronicDiseases = reader["ChronicDiseases"] as string ?? "",
                                EmergencyContactName = reader["EmergencyContactName"] as string ?? "",
                                EmergencyContactNumber = reader["EmergencyContactNumber"] as string ?? "",
                                InsuranceProvider = reader["InsuranceProvider"] as string ?? "",
                                InsuranceNumber = reader["InsuranceNumber"] as string ?? "",
                                MedicalHistoryNotes = reader["MedicalHistoryNotes"] as string ?? ""
                            };
                        }

                        // Return null if patient not found
                        return null;
                    }
                }
            }
        }
    }
}
