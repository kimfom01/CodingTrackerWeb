using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace CodingTrackerWeb.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public CodingHoursModel CodingHours { get; set; }

        public DeleteModel(IConfiguration configuration)
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

            DeleteRecord(id);

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
                        codingHours.Duration = (string)reader["Duration"];
                    }

                    return codingHours;
                }
            }
        }

        private void DeleteRecord(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @$"DELETE FROM coding_hours
                                            WHERE Id = {id}";

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
