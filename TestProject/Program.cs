using Microsoft.EntityFrameworkCore;

using TestProject.Infrastructure;

namespace TestProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Migrate(host.Services);
            host.Run();
        }

        public static void Migrate(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
