using Microsoft.AspNetCore.Identity;

namespace CodingTrackerWeb.Models;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public required string FirstName { get; set; }

    [PersonalData]
    public required string LastName { get; set; }

    public IEnumerable<CodingHour>? CodingHours { get; set; }
}
