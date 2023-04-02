using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CodingTrackerWeb.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ICodingHourRepository _repository;

    public List<CodingHour> Records { get; set; }

    public IndexModel(ICodingHourRepository repository)
    {
        _repository = repository;
    }

    public void OnGet()
    {
        var records = _repository.GetAllRecords();

        Records = records.OrderBy(x => x.Date).ToList();
    }
}