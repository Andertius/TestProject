using TestProject.Domain;
using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

namespace TestProject.Application.Services
{
    public class IncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IAccountRepository _accountRepository;

        public IncidentService(IIncidentRepository incidentRepo, IAccountRepository accountRepo, IContactRepository contactRepo)
        {
            _incidentRepository = incidentRepo;
            _accountRepository = accountRepo;
            _contactRepository = contactRepo;
        }

        public async Task<OperationResponse> CreateIncident(Incident incident, string accountName, Contact contact)
        {
            var account = await _accountRepository.FindAccountByName(accountName);

            if (account is not null)
            {
                var dBContact = await _contactRepository.FindContactByEmail(contact.Email);

                if (dBContact is null)
                {
                    account.Contact = contact;
                    await _contactRepository.AddContact(contact);
                    dBContact = contact;
                }

                if (dBContact.FirstName == contact.FirstName && dBContact.LastName == contact.LastName)
                {
                    incident.Account = account;
                    await _incidentRepository.AddIncident(incident);

                    await _incidentRepository.Commit();
                    await _contactRepository.Commit();

                    return new(OperationResult.Success);
                }

                return new(OperationResult.Failure, "Contact information was incorrect.");
            }

            return new(OperationResult.NotFound, "Account with specified name does not exist.");
        }
    }
}
