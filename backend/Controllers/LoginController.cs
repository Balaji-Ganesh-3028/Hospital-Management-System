using AppModels.Models;
using AppModels.RequestModels;
using AppModels.ResponseModels;
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

            // CONNECT TO DATABASE
            using SqlConnection connection = new SqlConnection(connectionString);

            // OPEN CONNECTION
            await connection.OpenAsync();

            // EXECUTE STORE PROCEDURE
            using SqlCommand command = new SqlCommand("sp_login", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // ADD PARAMETERS
            command.Parameters.AddWithValue("@email", request.email);
            command.Parameters.AddWithValue("@password", request.password);

            // EXECUTE READER
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            {
                // READ DATA
                if (reader.Read())
                {
                    var message = reader["message"].ToString();

                    // CHECK IF LOGIN IS SUCCESSFUL
                    if (message == AppConstants.DBResponse.LoginSuccessful)
                    {
                        // CREATE CLAIMS OBJECT
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

                        // RETURN SUCCESS RESPONSE WITH TOKEN
                        var token = GenerateToken.CreateToken(claims);
                        return StatusCode(200, new LoginResponse
                        {
                            Data = claims,
                            Txt = AppConstants.DBResponse.Success,
                            Message = $"{claims.userName} {AppConstants.ResponseMessages.LoggedInSuccessfully}",
                            Token = token
                        });
                    }
                    else // IF LOGIN FAILED
                    {
                        // HANDLE LOGIN FAILURE CASES
                        return message switch
                        {
                            AppConstants.DBResponse.InvalidCredentials => BadRequest(AppConstants.ResponseMessages.InvalidUserCredentials),
                            AppConstants.DBResponse.UserNotFound => NotFound(AppConstants.ResponseMessages.UserNotFound),
                            _ => BadRequest(AppConstants.ResponseMessages.LoginFailed)
                        };
                    }
                }

                // IF NO DATA RETURN LOGIN FAILED
                return NotFound(AppConstants.ResponseMessages.LoginFailed);
            }
        }
    }
}
