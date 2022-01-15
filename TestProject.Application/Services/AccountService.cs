using TestProject.Application.Repositories;
using TestProject.Domain;
using TestProject.Domain.Models;

namespace TestProject.Application.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IContactRepository _contactRepository;

        public AccountService(IAccountRepository accountRepo, IContactRepository contactRepo)
        {
            _accountRepository = accountRepo;
            _contactRepository = contactRepo;
        }

        public async Task<OperationResponse> CreateAccount(string accountName, string email)
        {
            var contact = await _contactRepository.FindContactByEmail(email);

            if (contact is not null)
            {
                if ((await _accountRepository.FindAccountByName(accountName)) is null)
                {
                    var account = new Account { Name = accountName };

                    account.Contact = contact;
                    await _accountRepository.AddAccount(account);
                    await _accountRepository.Commit();

                    return new(OperationResult.Success, "");
                }

                return new(OperationResult.Failure, "Account with specified name already exists.");
            }

            return new(OperationResult.NotFound, "Specified email is not registered.");
        }
    }
}
