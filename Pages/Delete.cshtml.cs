using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CodingTrackerWeb.Pages;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ICodingHourRepository _repository;

    [BindProperty]
    public CodingHour CodingHour { get; set; }

    public DeleteModel(ICodingHourRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet(int id)
    {
        CodingHour = _repository.GetById(id);

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _repository.DeleteRecord(id);

        return RedirectToPage("./Index");
    }
}
