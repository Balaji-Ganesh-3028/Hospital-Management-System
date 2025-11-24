using AppModels.RequestModels;
using Constant.Constants;
using DataAccessLayer.Interface;
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
            // CONNECT TO DATABASE
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                // EXECUTE STORED PROCEDURE
                using (SqlCommand command = new SqlCommand("sp_register", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // ADD PARAMETERS
                    command.Parameters.AddWithValue("@userName", request.username);
                    command.Parameters.AddWithValue("@email", request.email);
                    command.Parameters.AddWithValue("@passwordHash", request.password);
                    command.Parameters.AddWithValue("@roleId", request.roleId);

                    // READ DATA
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
    }
}
