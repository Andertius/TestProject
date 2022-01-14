using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestProject.Domain.Models;

namespace TestProject.Domain.Repositories
{
    public interface IIncidentRepository
    {
        Task AddIncident(Incident incident);

        Task Commit();
    }
}
