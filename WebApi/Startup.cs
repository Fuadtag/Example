using App;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistance;
using WebApi.Middlewares;

namespace WebApi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5000",
                            "https://localhost:5001");
                    });
            });
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-5.0#database-error-page
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            services.AddInfrastructure();
            services.AddPersistence(Configuration);
            services.AddApp();
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Pasha Life Insurance CRM Api", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("https://localhost:5001", "https://localhost:5000").AllowAnyMethod().AllowAnyHeader();
            });
            
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseCustomExceptionHandler();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseCustomExceptionHandler();
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // `UseOpenApi` & `UseSwaggerUi3` are required for swagger generation and ui
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}