using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

//Authentication and authorization

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

//Register Syncfusion license
SyncfusionLicenseProvider.RegisterLicense(
    "Mgo+DSMBMAY9C3t2VVhjQlFaclhJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRd0VgWH5WcHBWRWVdWUc=");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Account}/{action=Login}/{id?}");
app.MapRazorPages();
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();