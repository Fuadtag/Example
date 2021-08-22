using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance
{
    /// <summary>
    ///     This class provides a way to inject dependencies related to `Persistence`
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")
                    );
                }
            );

            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            return services;
        }
    }
}