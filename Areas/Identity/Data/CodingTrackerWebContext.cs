using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodingTrackerWeb.Data;

public class CodingTrackerWebContext : IdentityDbContext<IdentityUser>
{
    public DbSet<CodingHour> CodingHours { get; set; }

    public CodingTrackerWebContext(DbContextOptions<CodingTrackerWebContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
