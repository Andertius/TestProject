using TestProject.Application.Repositories;
using TestProject.Application.Services;
using TestProject.DataAccess.Repositories;

namespace TestProject.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            return collection
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IContactRepository, ContactRepository>()
                .AddScoped<IIncidentRepository, IncidentRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            return collection
                .AddScoped<AccountService>()
                .AddScoped<ContactService>()
                .AddScoped<IncidentService>();
        }
    }
}
