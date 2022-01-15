using TestProject.Domain.Models;

namespace TestProject.Application.Repositories
{
    public interface IAccountRepository
    {
        Task AddAccount(Account account);

        Task<Account> FindAccountByName(string name);

        Task Commit();
    }
}
