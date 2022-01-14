using Microsoft.EntityFrameworkCore;

using TestProject.Domain.Models;
using TestProject.Domain.Repositories;

namespace TestProject.DataAccess.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
        }

        public async Task<Contact> FindContactByEmail(string email)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Email == email);
        }

        public void UpdateContact(Contact contact)
        {
            _context.Update(contact);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
