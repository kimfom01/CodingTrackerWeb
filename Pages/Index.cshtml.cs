using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Data;

namespace CodingTrackerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDataAccess _dataAccess;
        public List<CodingHour> Records { get; set; }

        public IndexModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void OnGet()
        {
            var records = _dataAccess.GetAllRecords();
            
            Records = records.OrderBy(x => x.Date).ToList();
        }
    }
}