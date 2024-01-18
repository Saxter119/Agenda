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
    public interface IPhoneRepository
    {
        Task<Phone> getById(int? id);
        void DeleteById(int id);
         Task Add(Phone phone);
        Task SaveChangesAsync();
    }

    public class PhoneRepository : IPhoneRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PhoneRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Add(Phone phone)
        {
            await _dbContext.AddAsync(phone);
        }

        public async Task<Phone> getById(int? id)
        {
           var phone = await _dbContext.Phones.FirstOrDefaultAsync(phone=> phone.Id == id);

            return phone;

        }

        public void DeleteById(int id)
        {
            _dbContext.Remove(new Phone { Id = id });
           
            return;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
            return;
        }

    }

}
