using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Data;

namespace CodingTrackerWeb.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IDataAccess _dataAccess;

        [BindProperty]
        public CodingHours CodingHours { get; set; }

        public DeleteModel(IConfiguration configuration, IDataAccess dataAccess)
        {
            _configuration = configuration;
            _dataAccess = dataAccess;
        }

        public IActionResult OnGet(int id)
        {
            CodingHours = _dataAccess.GetById(id);

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dataAccess.DeleteRecord(id);

            return RedirectToPage("./Index");
        }
    }
}
