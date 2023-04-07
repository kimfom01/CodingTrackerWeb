using System.Security.Claims;
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

    public CodingHour? CodingHour { get; set; }

    public DeleteModel(ICodingHourRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet(int id)
    {
        var claimsIdentity = User.FindFirst(ClaimTypes.NameIdentifier);

        if (claimsIdentity is not null)
        {
            CodingHour = _repository.GetById(id) ?? new CodingHour
            {
                ApplicationUserId = claimsIdentity.Value
            };
        }
        
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
