using DataAccessLayer.Interface;
using DataAccessLayer.Models;
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
                        return "Success";
                    }
                    else
                    {
                        return "Failed";
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
                            patientList.Add(new
                            {
                                email = reader["Email"],
                                id = reader["UserId"],
                                userName = reader["UserName"],
                                firstName = reader["Firstname"],
                                lastName = reader["Lastname"],
                                gender = reader["Gender"],
                                age = reader["age"],
                                phoneNumber = reader["PhoneNumber"],
                                bloodGroup = reader["BloodGroup"],
                                DOJ = reader["DOJ"],
                                allergies = reader["Allergies"],
                                chronicDiseases = reader["ChronicDiseases"],
                                emergencyContactName = reader["EmergencyContactName"],
                                emergencyContactNumber = reader["EmergencyContactNumber"],
                                insuranceProvider = reader["InsuranceProvider"],
                                insuranceNumber = reader["InsuranceNumber"],
                                medicalHistoryNotes = reader["MedicalHistoryNotes"]
                            });
                        }

                        return patientList;
                    }
                }
            }
        }

        public async Task<object?> GetPatientDetails(int patientId)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_get_patient_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter for userId
                    cmd.Parameters.AddWithValue("@PatientId", patientId);

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
                                bloodGroup = reader["BloodGroup"],
                                DOJ = reader["DOJ"],
                                allergies = reader["Allergies"],
                                chronicDiseases = reader["ChronicDiseases"],
                                emergencyContactName = reader["EmergencyContactName"],
                                emergencyContactNumber = reader["EmergencyContactNumber"],
                                insuranceProvider = reader["InsuranceProvider"],
                                insuranceNumber = reader["InsuranceNumber"],
                                medicalHistoryNotes = reader["MedicalHistoryNotes"]
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
