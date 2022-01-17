using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using TestProject.Application.Services;
using TestProject.Infrastructure;
using TestProject.Middlewares;

namespace TestProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services
                .AddScoped<AccountService>()
                .AddScoped<ContactService>()
                .AddScoped<IncidentService>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddControllers()
               .AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
