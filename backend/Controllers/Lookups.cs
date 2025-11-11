using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("blood-group")]
        public async Task<IActionResult> GetBloodGroup()
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
                        cmd.Parameters.AddWithValue("@CategoryName", "blood_group");

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

        [AllowAnonymous]
        [HttpGet("appointment-type")]
        public async Task<IActionResult> GetAppointmentType()
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
                        cmd.Parameters.AddWithValue("@CategoryName", "appointment_type");

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

        [AllowAnonymous]
        [HttpGet("appointment-status")]
        public async Task<IActionResult> GetAppointmentStatus()
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
                        cmd.Parameters.AddWithValue("@CategoryName", "appointment_status");

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

        [AllowAnonymous]
        [HttpGet("specialisation")]
        public async Task<IActionResult> GetSpecialisation()
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
                        cmd.Parameters.AddWithValue("@CategoryName", "specialisation");

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

        [AllowAnonymous]
        [HttpGet("qualification")]
        public async Task<IActionResult> GetQualification()
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
                        cmd.Parameters.AddWithValue("@CategoryName", "qualification");

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


        [AllowAnonymous]
        [HttpGet("designation")]
        public async Task<IActionResult> GetDesignation()
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
                        cmd.Parameters.AddWithValue("@CategoryName", "designation");

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
