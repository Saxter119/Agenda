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
    }

    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ContactRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<List<Contact>> GetAll()
        {
            return await dbContext.Contacts.ToListAsync();
        }


        public async Task<Contact> Create(Contact contact) { 
            
            var createdContact = dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return contact;

        }

    }
}
