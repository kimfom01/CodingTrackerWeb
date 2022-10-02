using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace CodingTrackerWeb.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CodingHoursModel CodingHours { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            InsertRecord();

            return RedirectToPage("./Index");
        }

        private void InsertRecord()
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @$"INSERT INTO coding_hours(Date, StartTime, EndTime, Duration)
                                            VALUES('{CodingHours.Date}', '{CodingHours.StartTime}', '{CodingHours.EndTime}', '{CodingHours.GetDuration()}')";

                    command.ExecuteNonQuery();
                }
            }
        }

        public string GetDuration()
        {
            DateTime parsedStartTime = DateTime.ParseExact(CodingHours.StartTime, "HH:mm", null, DateTimeStyles.None);
            DateTime parsedEndTime = DateTime.ParseExact(CodingHours.EndTime, "HH:mm", null, DateTimeStyles.None);

            return parsedEndTime.Subtract(parsedStartTime).ToString();
        }
    }
}
