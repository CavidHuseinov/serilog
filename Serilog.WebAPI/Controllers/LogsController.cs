using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Serilog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LogsController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet("{date}")]
        public async Task<IActionResult> GetLogsByDate(string date = "04-05-2025")
        {
            if (!DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
            {
                return BadRequest("Tarixi duz yazin dd-MM-yyyy olaraq formatinda (04-05-2025)");
            }

            var connectionString = _config.GetConnectionString("Deploy");
            var logList = new List<object>();

            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"
                    SELECT TimeStamp, Level, Message, Exception 
                    FROM Logs 
                    WHERE CAST(TimeStamp AS DATE) = @date 
                    ORDER BY TimeStamp DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date", parsedDate.Date);

                    await connection.OpenAsync();  
                    using (var reader = await command.ExecuteReaderAsync())  
                    {
                        while (await reader.ReadAsync())  
                        {
                            var logItem = new
                            {
                                Time = reader["TimeStamp"].ToString(),
                                Level = reader["Level"].ToString(),
                                Message = reader["Message"].ToString(),
                                Exception = reader["Exception"]?.ToString()
                            };

                            logList.Add(logItem);
                        }
                    }
                }
            }
            return Ok(logList);
        }
    }
}
