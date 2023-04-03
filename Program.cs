using CodingTrackerWeb.Helper;
using CodingTrackerWeb.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CodingTrackerWeb.Data;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<CodingTrackerWebContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("LocalPostgreSQL"));
    });
}
else
{
    builder.Services.AddDbContext<CodingTrackerWebContext>(options =>
    {
        options.UseNpgsql(ExternalDbConnectionHelper.GetConnectionString());
    });
}

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CodingTrackerWebContext>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<ICodingHourRepository, CodingHourRepository>();

var app = builder.Build();

// Apply pending migrations on database
var scope = app.Services.CreateScope();
await DataHelper.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
