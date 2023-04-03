using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace CodingTrackerWeb.Models;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public required string FirstName { get; set; } // TODO: Figure out how to store extra data in Users table

    [PersonalData]
    public required string LastName { get; set; }

    public IEnumerable<CodingHour>? CodingHours { get; set; }
}
