using backend.models;
using backend.utilities;
using Constant.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace backend.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration congiguration)
        {
            _configuration = congiguration;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = string.Empty;
            string connectionString = _configuration["ConnectionStrings:DB"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("sp_login", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", request.email);
                    command.Parameters.AddWithValue("@password", request.password);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read() && reader["message"].ToString() == AppConstants.DBResponse.LoginSuccessful)
                            {
                                var userName = reader["userName"].ToString();
                                var claims = new ClaimsItems
                                {
                                    roleId = reader["roleId"].ToString(),
                                    userName = reader["UserName"].ToString(),
                                    email = reader["Email"].ToString(),
                                    roleName = reader["RoleName"].ToString(),
                                    userId = (int)reader["Id"],
                                    UserType = reader["UserType"].ToString(),
                                    UserTypeId = (int)reader["UserTypeId"]
                                };

                                token = GenerateToken.CreateToken(claims);
                                return StatusCode(200, new
                                {
                                    data = claims,
                                    txt = AppConstants.DBResponse.Success,
                                    message = userName + AppConstants.ResponseMessages.LoggedInSuccessfully,
                                    token = token
                                });
                            }
                            return BadRequest(AppConstants.ResponseMessages.LoginFailed);
                        }
                        else
                        {
                            return BadRequest(AppConstants.ResponseMessages.LoginFailed);
                        }


                    }
                }
            }
        }
    }
}
