using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CodingTrackerWeb.Pages;

[Authorize]
public class CreateModel : PageModel
{
    [BindProperty]
    public CodingHour? CodingHour { get; set; }

    private readonly ICodingHourRepository _repository;

    public CreateModel(ICodingHourRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet()
    {
        var claimsIdentity = User.FindFirst(ClaimTypes.NameIdentifier);

        CodingHour = new()
        {
            ApplicationUserId = claimsIdentity.Value
        };

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _repository.InsertRecord(CodingHour);

        return RedirectToPage("./Index");
    }
}
