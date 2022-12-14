using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Data;

namespace CodingTrackerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IDataAccess _dataAccess;
        public List<CodingHour> Records { get; set; }

        public IndexModel(IConfiguration configuration, IDataAccess dataAccess)
        {
            _configuration = configuration;
            _dataAccess = dataAccess;
        }

        public void OnGet()
        {
            Records = _dataAccess.GetAllRecords();
        }
    }
}