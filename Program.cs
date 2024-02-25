using AccountDeletion.Extensions;
using AccountDeletion.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
            .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AccountDeletionDbContext>(options =>
    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));
builder.Services.APIModuleServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
