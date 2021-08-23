using System.Reflection;
using App.Products;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApp(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}