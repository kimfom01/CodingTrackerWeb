using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace CodingTrackerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<CodingHoursModel> Records { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Records = GetAllRecords();
        }

        public List<CodingHoursModel> GetAllRecords()
        {
            var codingHoursModels = new List<CodingHoursModel>();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "SELECT * FROM coding_hours";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var tempModel = new CodingHoursModel();

                        tempModel.Id = reader.GetInt32(0);
                        tempModel.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                        tempModel.StartTime = (string)reader["StartTime"];
                        tempModel.EndTime = (string)reader["EndTime"];
                        tempModel.Duration = (string)reader["Duration"];

                        codingHoursModels.Add(tempModel);
                    }
                }
            }

            return codingHoursModels;
        }
    }
}