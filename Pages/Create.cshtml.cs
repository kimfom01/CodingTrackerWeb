using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Data;

namespace CodingTrackerWeb.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IDataAccess _dataAccess;
        [BindProperty]
        public CodingHour CodingHour { get; set; }

        public CreateModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
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

            _dataAccess.InsertRecord(CodingHour);

            return RedirectToPage("./Index");
        }
    }
}
