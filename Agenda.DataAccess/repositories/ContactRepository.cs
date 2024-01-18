using Agenda.BusinessLogic.entities;
using Agenda.Presentation.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DataAccess.repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAll();
        Task<Contact> Create(Contact contact);
        Task<Contact> GetById(int id);
        Task SaveChangesAsync();
    }

    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Contact> GetById(int id)
        {
            var contact = await _dbContext.Contacts
                                          .Include(x => x.Phones)
                                          .Include(x => x.Emails)
                                         .FirstOrDefaultAsync(contact => contact.Id == id);
            return contact;
        }

        public async Task<List<Contact>> GetAll()
        {
            return await _dbContext.Contacts.Where(contact=> contact.Deleted != true)
                                  .Include(x => x.Emails)
                                  .Include(x => x.Phones)
                                  .ToListAsync();
        }


        public async Task<Contact> Create(Contact contact)
        {

            var createdContact = _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return contact;

        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
            return;
        }
    }
}
