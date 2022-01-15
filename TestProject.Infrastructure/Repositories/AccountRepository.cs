using Microsoft.EntityFrameworkCore;

using TestProject.Application.Repositories;
using TestProject.Domain.Models;

namespace TestProject.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public async Task<Account> FindAccountByName(string name)
        {
            return await _context.Accounts
                .Where(x => x.Name == name)
                .Include(x => x.Contact)
                .FirstOrDefaultAsync();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
