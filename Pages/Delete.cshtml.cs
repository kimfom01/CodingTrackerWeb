using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Data;

namespace CodingTrackerWeb.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IDataAccess _dataAccess;

        [BindProperty]
        public CodingHour CodingHour { get; set; }

        public DeleteModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IActionResult OnGet(int id)
        {
            CodingHour = _dataAccess.GetById(id);

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
