using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Lookups : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Lookups(IConfiguration configuration) { 
            _configuration = configuration;
        }

        // TO GET ROLES
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            string connectionString = _configuration["ConnectionStrings:DB"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_get_roles", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var roles = new List<object>();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                roles.Add(new
                                {
                                    id = reader["Id"],
                                    roleName = reader["RoleName"],
                                    roleCode = reader["RoleCode"]
                                });
                            }

                            return Ok(roles);
                        }

                    }
                } catch (SqlException sqlEx)
                {
                    return StatusCode(500, "Sql server error: " + sqlEx.Message);
                }

            }
        }


        // TO GET GENDER
        [HttpGet("gender")]
        public async Task<IActionResult> GetGender()
        {
            string connectionString = _configuration["ConnectionStrings:DB"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetMasterData", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryName", "gender");

                        var gender = new List<object>();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                gender.Add(new
                                {
                                    id = reader["Id"],
                                    name = reader["Name"],
                                    value = reader["Value"]
                                });
                            }

                            return Ok(gender);
                        }

                    }
                }
                catch (SqlException sqlEx)
                {
                    return StatusCode(500, "Sql server error: " + sqlEx.Message);
                }

            }
        }
    }
}
