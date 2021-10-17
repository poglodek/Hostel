using Hostel_System;
using Hostel_System.Core.IServices;
using Hostel_System.Core.Services;
using Hostel_System.Database;
using Hostel_System.Database.Entity;
using Hostel_System.Dto;
using Hostel_System.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HostelSystemDbContext>(options =>
    options.UseSqlServer("Server=.;Database=HostelSystem;Trusted_Connection=True;"));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Login";
});
builder.Services.AddAutoMapper(typeof(Hostel_System.Dto.HostelSystemMapper).Assembly);
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRoomServices, RoomServices>();
builder.Services.AddScoped<IUserContextServices, UserContextServices>();
builder.Services.AddScoped<IReservationServices, ReservationServices>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); 
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddTransient<ErrorHandlingMiddleware>();
builder.Services.AddTransient<HostelSystemModelMapper>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<ErrorHandlingMiddleware>();
var scope = app.Services.CreateScope();
scope.ServiceProvider.GetService<HostelSystemDbContext>().Database.Migrate();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
