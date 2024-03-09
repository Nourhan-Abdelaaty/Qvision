using Application.Services.Interfaces;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using MVC.Application.Services.Implementations.Products;
using MVC.Application.Services.Interfaces.Products;
using MVC.DataAccess.Context;
using MVC.Application.Services.Interfaces.User;
using MVC.Application.Services.Implementations.User;

namespace MVC.Presentation.Helper
{
    public static class ServicesLifeTimeService
    {
        public static IServiceCollection AddServicesLifeTime(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<DataContext>();
            #region ProductService
               service.AddScoped<IProductService, ProductService>();
            #endregion
            #region AuthenticationService
               service.AddScoped<IUserService, UserService>();
            #endregion
            return service;
        }
    }

}
