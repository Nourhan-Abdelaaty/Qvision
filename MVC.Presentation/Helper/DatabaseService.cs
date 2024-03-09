using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.DataAccess.Context;

namespace MVC.Presentation.Helper
{

    public static partial class DatabaseService
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(60));
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
       
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
             });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login"; 
            });
            return services;
        }
    }
}
