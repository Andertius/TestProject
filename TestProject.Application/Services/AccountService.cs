using Microsoft.EntityFrameworkCore;

using TestProject.Domain.Models;
using TestProject.Domain.Requests;
using TestProject.Domain.Responses;
using TestProject.Infrastructure;

namespace TestProject.Application.Services
{
    public class AccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Account>> CreateAccount(AccountRequest request)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (contact is not null)
            {
                if ((await _context.Accounts.FirstOrDefaultAsync(x => x.Name == request.AccountName)) is null)
                {
                    var account = new Account { Name = request.AccountName };

                    await _context.Accounts.AddAsync(account);
                    contact.Account = account;
                    await _context.SaveChangesAsync();

                    return new(account, OperationResult.Success, "");
                }

                return new(null, OperationResult.Failure, "Account with specified name already exists.");
            }

            return new(null, OperationResult.NotFound, "Specified email is not registered.");
        }
    }
}
