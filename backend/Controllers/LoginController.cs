using AppModels.Models;
using AppModels.RequestModels;
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
            string connectionString = _configuration["ConnectionStrings:DB"];
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand("sp_login", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@email", request.email);
            command.Parameters.AddWithValue("@password", request.password);

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            {
                if (reader.Read())
                {
                    var message = reader["message"].ToString();
                    if (message == AppConstants.DBResponse.LoginSuccessful)
                    {
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

                        var token = GenerateToken.CreateToken(claims);
                        return StatusCode(200, new
                        {
                            data = claims,
                            txt = AppConstants.DBResponse.Success,
                            message = $"{claims.userName} {AppConstants.ResponseMessages.LoggedInSuccessfully}",
                            token
                        });
                    }
                    else
                    {
                        return message switch
                        {
                            AppConstants.DBResponse.InvalidCredentials => BadRequest(AppConstants.ResponseMessages.InvalidUserCredentials),
                            AppConstants.DBResponse.UserNotFound => BadRequest(AppConstants.ResponseMessages.UserNotFound),
                            _ => BadRequest(AppConstants.ResponseMessages.LoginFailed)
                        };
                    }
                }
                return BadRequest(AppConstants.ResponseMessages.LoginFailed);
            }
        }
    }
}
