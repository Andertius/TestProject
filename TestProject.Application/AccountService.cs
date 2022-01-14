using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

namespace TestProject.Application
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

        public async Task<bool> CreateAccount(Account account, string email)
        {
            var contact = await _contactRepository.FindContactByEmail(email);

            if (contact is not null)
            {
                account.Contact = contact;
                await _accountRepository.AddAccount(account);
                await _accountRepository.Commit();
                return true;
            }

            return false;
        }
    }
}
