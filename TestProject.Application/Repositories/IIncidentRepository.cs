using TestProject.Domain.Models;

namespace TestProject.Application.Repositories
{
    public interface IIncidentRepository
    {
        Task AddIncident(Incident incident);

        Task Commit();
    }
}
