using CodingTrackerWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace CodingTrackerWeb.Helper;

public class DataHelper
{
    public static async Task ManageDataAsync(IServiceProvider svcProvider)
    {
        //Service: An instance of db context
        var dbContextSvc = svcProvider.GetRequiredService<CodingTrackerWebContext>();

        //Migration: This is the programmatic equivalent to Update-Database
        var pendingMigrations = await dbContextSvc.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            await dbContextSvc.Database.MigrateAsync();
        }
    }
}