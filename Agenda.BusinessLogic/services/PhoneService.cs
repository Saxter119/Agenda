using Agenda.DataAccess.entities;
using Agenda.DataAccess.repositories;
using Agenda.Presentation.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.services
{
    public interface IPhoneService
    {
        Task DeletePhoneById(int id);
        Task<Phone> GetPhoneById(int? id);
    }
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository phoneRepository;

        public PhoneService(IPhoneRepository phoneRepository)
        {
            this.phoneRepository = phoneRepository;
        }

        public async Task<Phone> GetPhoneById(int? id)
        {
            var phone = await phoneRepository.getById(id);

            if (phone == null) throw new Exception("phone not found");

            return phone;
        }

        public async Task DeletePhoneById(int id)
        {
            phoneRepository.DeleteById(id);
            await phoneRepository.SaveChangesAsync();
            return;
        }
    }
}
