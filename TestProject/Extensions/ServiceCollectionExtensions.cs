using TestProject.Application.Services;
using TestProject.DataAccess.Repositories;
using TestProject.Domain.Repositories;

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
