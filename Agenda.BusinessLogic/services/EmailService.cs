using Agenda.BusinessLogic.dtos;
using Agenda.DataAccess.entities;
using Agenda.DataAccess.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.services
{
    public interface IEmailService
    {
        Task<Email> GetEmailByid(int? id);
        Task DeleteEmailById(int id);
    }
    public class EmailService : IEmailService
    {

        private readonly IEmailRepository emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public async Task<Email> GetEmailByid(int? id)
        {
            var email = await emailRepository.getById(id);

            if (email == null) throw new Exception("email not found");

            return email;
        }

        public async Task DeleteEmailById(int id)
        {
            var email = emailRepository.getById(id);

            if (email is null) throw new Exception("Email not found");

            emailRepository.DeleteById(id);

            await emailRepository.SaveChangesAsync();
        }
    }


}
