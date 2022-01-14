using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

namespace TestProject.DataAccess.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly AppDbContext _context;

        public IncidentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddIncident(Incident incident)
        {
            throw new NotImplementedException();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
