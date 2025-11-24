using backend.CustomAttributes;
using backend.Enum;
using Constant.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionStrings:DB"];
        }

        [HttpGet]
        [CustomAuth(Roles.Admin, Roles.FrontDesk)]
        public async Task<IActionResult> GetDashboardDetails()
        {
            // CONNECT TO DATABASE
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                // OPEN CONNECTION
                await connection.OpenAsync();

                // EXECUTE STORED PROCEDURE
                using (SqlCommand command = new SqlCommand("sp_get_dashboard_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // READ DATA
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            var data = new
                            {
                                PatientCount = reader["patientCount"],
                                DoctorCount = reader["DoctorCount"],
                                AppointmentCount = reader["AppointmentCount"],
                                TotalUserCount = reader["TotalUserCount"],
                                PatientAddedToday = reader["PatientAddedToday"],
                                DoctorAddedToday = reader["DoctorAddedToday"],
                                TotalAppointmentsAddedToday = reader["TotalAppointmentsAddedToday"]
                            };

                            return Ok(data);
                        } else
                        {
                            return BadRequest(AppConstants.ResponseMessages.SomethingWentWrong);
                        }
                    }
                }
            }
        }
    }
}
