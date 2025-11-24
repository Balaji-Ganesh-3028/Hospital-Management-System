using AppModels.Models;
using AppModels.ResponseModels;
using Constant.Constants;
using DataAccessLayer.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLayer.Implementation
{
    public class AppointmentDAL : IAppointmentDAL
    {
        private readonly IConfiguration _configuration;
        private readonly string _dbConnectionString;

        public AppointmentDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnectionString = _configuration["ConnectionStrings:DB"];
        }
        public async Task<List<AppointmentDetailsResponse>> GellAllAppointment()
        {
            var appointmentList = new List<AppointmentDetailsResponse>();

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_get_all_appointments", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var appointment = new AppointmentDetailsResponse
                            {
                                AppointmentDetails = new AppointmentDetailsInfo
                                {
                                    AppointmentId = reader["AppointmentId"] as int? ?? 0,
                                    AppointmentDate = DateOnly.FromDateTime((DateTime)reader["AppointmentDate"]),
                                    PurposeOfVisitName = reader["PurposeOfVisitName"] as string,
                                    PurposeOfVisit = reader["PurposeOfVisit"] as int? ?? 0,
                                    IllnessOrDisease = reader["IllnessOrDisease"] as string,
                                    ProceduresOrMedication = reader["ProceduresOrMedication"] as string,
                                    CurrentStatus = reader["CurrentStatus"] as int? ?? 0,
                                    Status = reader["Status"] as string,
                                },
                                PatientInfo = new PatientInfo
                                {
                                    PatientId = reader["PatientId"] as int? ?? 0,
                                    PatientFirstName = reader["PatientFirstName"] as string,
                                    PatientLastName = reader["PatientLastName"] as string,
                                },
                                DoctorInfo = new DoctorInfo
                                {
                                    DoctorId = reader["DoctorId"] as int? ?? 0,
                                    DoctorFirstName = reader["DoctorFirstName"] as string,
                                    DoctorLastName = reader["DoctorLastName"] as string,
                                }
                            };

                            appointmentList.Add(appointment);
                        }

                    }
                }
            }
            return appointmentList;
        }

        public async Task<AppointmentDetailsResponse> GellAppointment(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_get_appointment_details_by_id", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new AppointmentDetailsResponse
                            {
                                AppointmentDetails = new AppointmentDetailsInfo
                                {
                                    AppointmentId = reader["AppointmentId"] as int? ?? 0,
                                    AppointmentDate = DateOnly.FromDateTime((DateTime)reader["AppointmentDate"]),
                                    PurposeOfVisitName = reader["PurposeOfVisitName"] as string,
                                    PurposeOfVisit = reader["PurposeOfVisit"] as int? ?? 0,
                                    IllnessOrDisease = reader["IllnessOrDisease"] as string,
                                    ProceduresOrMedication = reader["ProceduresOrMedication"] as string,
                                    CurrentStatus = reader["CurrentStatus"] as int? ?? 0,
                                    Status = reader["Status"] as string,
                                },

                                PatientInfo = new PatientInfo
                                {
                                    PatientId = reader["PatientId"] as int? ?? 0,
                                    PatientFirstName = reader["PatientFirstName"] as string,
                                    PatientLastName = reader["PatientLastName"] as string,
                                    PatientBloodGroup = reader["BloodGroupName"] as string ?? ""
                                },

                                DoctorInfo = new DoctorInfo
                                {
                                    DoctorId = reader["DoctorId"] as int? ?? 0,
                                    DoctorFirstName = reader["DoctorFirstName"] as string,
                                    DoctorLastName = reader["DoctorLastName"] as string,
                                    Specialization = reader["Specialisation"] as string,
                                    Designation = reader["Designation"] as string
                                }
                            };
                        }
                    }
                }

                await connection.CloseAsync();
            }

            return null;
        }

        public async Task<string> InsertUpdateAppointmrnt(Appointment appointment)
        {
            using(SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync();
                using(SqlCommand cmd = new SqlCommand("sp_update_patient_appointment_details", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ADD VALUES WITH PARAMETERS
                    cmd.Parameters.AddWithValue("@id", appointment.Id);
                    cmd.Parameters.AddWithValue("@patientID", appointment.PatientId);
                    cmd.Parameters.AddWithValue("@doctorID", appointment.DoctorId);
                    cmd.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                    cmd.Parameters.AddWithValue("@illnessOrDisease", appointment.IllnessOrDisease);
                    cmd.Parameters.AddWithValue("@proceduresOrMedication", appointment.ProceduresOrMedication);
                    cmd.Parameters.AddWithValue("@currentStatus", appointment.CurrentStatus);
                    cmd.Parameters.AddWithValue("@purposeOfVisit", appointment.PurposeOfVisit);


                    // IF CREATEBY IS NOT EMPTY THEN ADD PARAMETER
                    if (appointment.CreatedBy != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@createdBy", appointment.CreatedBy);
                    }

                    // IF UPDATEBY IS NOT EMPTY THEN ADD PARAMETER
                    if (appointment.UpdatedBy != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@updatedBy", appointment.UpdatedBy);
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
