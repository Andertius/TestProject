using TestProject.Application.Repositories;
using TestProject.Domain.Models;

namespace TestProject.DataAccess.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly AppDbContext _context;

        public IncidentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddIncident(Incident incident)
        {
            await _context.AddAsync(incident);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
