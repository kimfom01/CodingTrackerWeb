using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace CodingTrackerWeb.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public CodingHoursModel CodingHours { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            CodingHours = GetById(id);

            return Page();
        }


        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UpdateRecord(id);

            return RedirectToPage("./Index");
        }

        private CodingHoursModel GetById(int id)
        {
            using var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString"));
            {
                using (var command = connection.CreateCommand())
                {
                    var codingHours = new CodingHoursModel();

                    connection.Open();

                    command.CommandText = @$"SELECT * FROM coding_hours
                                            WHERE Id = {id}";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        codingHours.Id = reader.GetInt32(0);
                        codingHours.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                        codingHours.StartTime = (string)reader["StartTime"];
                        codingHours.EndTime = (string)reader["EndTime"];
                    }

                    return codingHours;
                }
            }
        }

        private void UpdateRecord(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @$"UPDATE coding_hours
                                            SET Date = '{CodingHours.Date}', StartTime = '{CodingHours.StartTime}', EndTime = '{CodingHours.EndTime}', Duration = '{CodingHours.GetDuration()}'
                                            WHERE Id = {id}";
                    
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
