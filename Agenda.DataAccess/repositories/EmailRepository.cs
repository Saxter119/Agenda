using Agenda.DataAccess.entities;
using Agenda.Presentation.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DataAccess.repositories
{
    public interface IEmailRepository
    {
        void DeleteById(int id);
        Task<Email> getById(int? id);
        Task SaveChangesAsync();
        Task Add(Email email);
    }
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Email> getById(int? id)
        {
            var email = await _dbContext.Emails.FirstOrDefaultAsync(email => email.Id == id);

            return email;
        }

        public async Task Add(Email email)
        {
            await _dbContext.AddAsync(email);
        }

        public void DeleteById(int id)
        {
            _dbContext.Remove(new Email { Id = id });

            return;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
            return;
        }
    }


}
