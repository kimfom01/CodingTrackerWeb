using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Data;

namespace CodingTrackerWeb.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IDataAccess _dataAccess;
        [BindProperty]
        public CodingHours CodingHours { get; set; }

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dataAccess.InsertRecord(CodingHours);

            return RedirectToPage("./Index");
        }
    }
}
