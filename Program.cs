using CodingTrackerWeb.Context;
using CodingTrackerWeb.Data;
using CodingTrackerWeb.Helper;
using Microsoft.EntityFrameworkCore;

namespace CodingTrackerWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<IDataAccess, EntityFrameworkDataAccess>();
            builder.Services.AddDbContext<CodingHoursContext>(options =>
            {
                // options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString"));
                options.UseNpgsql(ExternalDbConnectionHelper.GetConnectionString());
            });

            var app = builder.Build();
            
            // Apply pending migrations on database
            var scope = app.Services.CreateScope();
            await DataHelper.ManageDataAsync(scope.ServiceProvider);
            
            // posgress date error fix?
            // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapRazorPages();

            app.Run();
        }
    }
}