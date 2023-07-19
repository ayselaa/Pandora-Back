using Domain;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Helpers;
using Service.Interfaces;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddServiceLayer();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Aws"));
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireNonAlphanumeric = false;

    option.User.RequireUniqueEmail = true;

    option.Lockout.MaxFailedAccessAttempts = 3;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    option.Lockout.AllowedForNewUsers = true;

});


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

app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
