using CodingTrackerWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTrackerWeb.Context;

public class CodingHoursContext : DbContext
{
    public DbSet<CodingHour> CodingHours { get; set; }

    public CodingHoursContext(DbContextOptions<CodingHoursContext> options) : base(options)
    {
        
    }
}