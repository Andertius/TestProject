using Microsoft.EntityFrameworkCore;

using TestProject.Domain.Models;
using TestProject.Domain.Requests;
using TestProject.Domain.Responses;
using TestProject.Infrastructure;

namespace TestProject.Application.Services
{
    public class ContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Contact>> CreateContact(ContactRequest request)
        {
            var contact = new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

            if ((await _context.Contacts.FirstOrDefaultAsync(x => x.Email == contact.Email)) is null)
            {
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();

                return new(contact, OperationResult.Success);
            }

            return new(contact, OperationResult.Failure, "Specified email address already exists.");
        }
    }
}
