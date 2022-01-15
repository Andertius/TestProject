using TestProject.Domain.Models;

namespace TestProject.Application.Repositories
{
    public interface IContactRepository
    {
        Task AddContact(Contact contact);

        Task<Contact> FindContactByEmail(string email);

        void UpdateContact(Contact contact);

        Task Commit();
    }
}
