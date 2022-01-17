using Microsoft.EntityFrameworkCore;

using TestProject.Domain.Models;
using TestProject.Domain.Requests;
using TestProject.Domain.Responses;
using TestProject.Infrastructure;

namespace TestProject.Application.Services
{
    public class IncidentService
    {
        private readonly AppDbContext _context;

        public IncidentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Incident>> CreateIncident(IncidentRequest request)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Name == request.AccountName);
            var contact = new Contact { Email = request.Email, FirstName = request.FirstName, LastName = request.LastName };
            var incident = new Incident { Description = request.Description, Name = Guid.NewGuid().ToString() };

            if (account is not null)
            {
                var dbContact = await _context.Contacts.FirstOrDefaultAsync(x => x.Email == contact.Email);

                if (dbContact is null)
                {
                    contact.Account = account;
                    await _context.Contacts.AddAsync(contact);
                    dbContact = contact;
                }

                if (dbContact.FirstName == contact.FirstName && dbContact.LastName == contact.LastName)
                {
                    await _context.Incidents.AddAsync(incident);
                    account.Incident = incident;

                    await _context.SaveChangesAsync();

                    return new(incident, OperationResult.Success);
                }

                return new(null, OperationResult.Failure, "Contact information was incorrect.");
            }

            return new(null, OperationResult.NotFound, "Account with specified name does not exist.");
        }
    }
}
