using Constant.Constants;
using DataAccessLayer.Interface;
using DataAccessLayer.models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLayer.Implementation
{
    public class RegisterDAL : IRegisterDAL
    {
        private readonly IConfiguration _connectionString;
        private readonly string _dbConnectionString;
        public RegisterDAL(IConfiguration configuration)
        {
            _connectionString = configuration;
            _dbConnectionString = _connectionString["ConnectionStrings:DB"];
        }

        public async Task<string> RegisterUser(RegisterRequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_register", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userName", request.username);
                        command.Parameters.AddWithValue("@email", request.email);
                        command.Parameters.AddWithValue("@passwordHash", request.password);
                        command.Parameters.AddWithValue("@roleId", request.roleId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read() && reader["message"]?.ToString() == "Registered")
                            {
                                return AppConstants.DBResponse.Success;
                            }
                            return AppConstants.DBResponse.Failed;
                        }
                    }

                }
            }
            catch (SqlException sqlEx)
            {
                return AppConstants.ResponseMessages.DatabaseError + sqlEx.Message;
            }
            catch (Exception ex)
            {
                return AppConstants.ResponseMessages.InternalServerError + ex.Message;
            }
        }
    }
}
