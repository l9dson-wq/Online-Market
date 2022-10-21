using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using StoackApp.Core.Application.Interfaces.Services;

namespace StockApp.Infrastructure.Persistence
{
    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
