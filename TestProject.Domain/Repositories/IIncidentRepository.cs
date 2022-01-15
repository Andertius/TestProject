using TestProject.Domain.Models;

namespace TestProject.Domain.Repositories
{
    public interface IIncidentRepository
    {
        Task AddIncident(Incident incident);

        Task Commit();
    }
}
