using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QlvuKhiTrangBiContext>(options =>
    options.UseSqlServer("Data Source=NGUYEN-THUY-QUY\\QUYNH;Initial Catalog=QLVuKhiTrangBi;Encrypt=false;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Login/Index";
            options.LogoutPath = "/Login/Logout";
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireTieuDoan", builder => builder.RequireRole("tieudoan"));
    options.AddPolicy("RequireDaiDoi", builder => builder.RequireRole("daidoi"));
    options.AddPolicy("RequireTroLy", builder => builder.RequireRole("troly"));
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
