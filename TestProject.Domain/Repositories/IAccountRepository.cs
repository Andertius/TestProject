using TestProject.Domain.Models;

namespace TestProject.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task AddAccount(Account account);

        Task<Account> FindAccountByName(string name);

        Task Commit();
    }
}
