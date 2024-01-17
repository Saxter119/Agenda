using Agenda.BusinessLogic.dtos;
using Agenda.BusinessLogic.entities;
using Agenda.DataAccess.entities;
using Agenda.DataAccess.repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.services
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetContactsDto();
        Task CreateContact(ContactCreationDto contactCreationDto);
    }
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;

        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            this.contactRepository = contactRepository;
            this.mapper = mapper;
        }

        public async Task<List<ContactDto>> GetContactsDto()
        {
            var contacts = await contactRepository.GetAll();

            var contactsDto = new List<ContactDto>();

            foreach (var contact in contacts)
            {
                contactsDto.Add(new ContactDto
                {
                    Name = contact.Name,
                    LastName = contact.LastName,
                    Direction = contact.Direction,
                    NationalId = contact.NationalId,
                });
            }

            return contactsDto;
        }

        public async Task CreateContact(ContactCreationDto contactCreationDto)
        {
            try
            {
                if (contactCreationDto == null) throw new ArgumentNullException();

                if (contactCreationDto.Phones == null && contactCreationDto.Emails == null) throw new ArgumentNullException(
                    "A contact has to be created with at least one number or email address");

                var contact = new Contact()
                {
                    Name = contactCreationDto.Name,
                    LastName = contactCreationDto.LastName,
                    Direction = contactCreationDto.Direction,
                    NationalId = contactCreationDto.NationalId,
                    Emails = MapDtoToEmail(contactCreationDto.Emails),
                    Phones = MapDtoToPhone(contactCreationDto.Phones)
                };

                await contactRepository.Create(contact);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            //return contactCreationDto;

        }

        private List<Email> MapDtoToEmail(List<EmailDto> emailsDto)
        {
            var emails = new List<Email>();

            foreach (var email in emailsDto)
            {
                emails.Add(new Email() { EmailAddress = email.EmailContact });
            }

            return emails;
        }

        private List<Phone> MapDtoToPhone(List<PhoneDto> phonesDto)
        {
            var phones = new List<Phone>();

            foreach (var phone in phonesDto)
            {
                phones.Add(new Phone() { Number = phone.Number });
            }

            return phones;
        }
    }
}
