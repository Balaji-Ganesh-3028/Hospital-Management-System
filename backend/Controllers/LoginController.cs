using backend.models;
using backend.utilities;
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

            try
            {
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
                                if (reader.Read() && reader["message"].ToString() == "Login successful")
                                {
                                    var userName = reader["userName"].ToString();
                                    var claims = new ClaimsItems
                                    {
                                        roleId = reader["roleId"].ToString(),
                                        userName = reader["UserName"].ToString(),
                                        email = reader["Email"].ToString(),
                                        roleName = reader["RoleName"].ToString(),
                                        userId = (int)reader["id"]
                                    };

                                    token = GenerateToken.CreateToken(claims);
                                    return StatusCode(200, new
                                    {
                                        data = claims,
                                        txt = "Success",
                                        message = userName + " logged in successfully.",
                                        token = token
                                    });
                                }
                                return BadRequest("Login failed, Something went wrong!!");
                            }
                            else
                            {
                                Console.WriteLine("No rows found.");
                                return BadRequest("Login failed, Something went wrong!!");
                            }


                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return BadRequest("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Internal server error: " + ex.Message);
            }
        }

    }
}
