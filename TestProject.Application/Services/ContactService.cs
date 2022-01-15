using TestProject.Domain;
using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

namespace TestProject.Application.Services
{
    public class ContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResponse> CreateContact(Contact contact)
        {
            if ((await _repository.FindContactByEmail(contact.Email)) is null)
            {
                await _repository.AddContact(contact);
                await _repository.Commit();

                return new(OperationResult.Success);
            }

            return new(OperationResult.Failure, "Specified email address already exists.");
        }
    }
}
