using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //TODO fix
        public async Task<OperationResponse> CreateIncident(Incident incident, string accountName, Contact contact)
        {
            var account = await _accountRepository.FindAccountByName(accountName);

            if (account is not null)
            {
                var dBContact = await _contactRepository.FindContactByEmail(contact.Email);

                if (dBContact is null)
                {
                    await _contactRepository.AddContact(contact);
                    dBContact = contact;
                }

                if (dBContact.FirstName == contact.FirstName && dBContact.LastName == contact.LastName)
                {
                    dBContact.Accounts.Add(account);

                    await _incidentRepository.AddIncident(incident);
                    account.Incidents.Add(incident);

                    await _incidentRepository.Commit();
                    await _accountRepository .Commit();
                    await _contactRepository.Commit();

                    return new(OperationResult.Success);
                }
            }

            return new(OperationResult.NotFound, "Account with specified name does not exist.");
        }
    }
}
