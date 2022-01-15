using TestProject.Domain;
using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

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

        public async Task<OperationResponse> CreateAccount(Account account, string email)
        {
            var contact = await _contactRepository.FindContactByEmail(email);

            if (contact is not null)
            {
                if ((await _accountRepository.FindAccountByName(account.Name)) is null)
                {
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
