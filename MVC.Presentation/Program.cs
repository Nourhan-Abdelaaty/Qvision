using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVC.Application.Services.Implementations.User;
using MVC.Application.Services.Interfaces.User;
using MVC.DataAccess.Context;
using MVC.Presentation.Helper;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration && Identity
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDatabase(connectionString);

//Add Dependency Injecttion
builder.Services.AddServicesLifeTime();

builder.Services.AddControllersWithViews();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
