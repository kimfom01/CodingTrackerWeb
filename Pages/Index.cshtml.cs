using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CodingTrackerWeb.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ICodingHourRepository _repository;

    public List<CodingHour> Records { get; set; }

    public IndexModel(ICodingHourRepository repository)
    {
        _repository = repository;
        Records = new List<CodingHour>();
    }

    public void OnGet()
    {
        var claimsIdentity = User.FindFirst(ClaimTypes.NameIdentifier);

        if (claimsIdentity is not null)
        {
            var records = _repository.GetUserRecords(codingHour => 
                codingHour.ApplicationUserId == claimsIdentity.Value);
            
            Records = records.OrderBy(x => x.Date).ToList();
        }
    }
}