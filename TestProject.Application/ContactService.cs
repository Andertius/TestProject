using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

namespace TestProject.Application
{
    public class ContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateContact(Contact contact)
        {
            if ((await _repository.FindContactByEmail(contact.Email)) is null)
            {
                await _repository.AddContact(contact);
                await _repository.Commit();
            }

            return false;
        }
    }
}
