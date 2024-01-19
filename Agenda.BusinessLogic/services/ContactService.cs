using Agenda.BusinessLogic.dtos;
using Agenda.BusinessLogic.entities;
using Agenda.DataAccess.entities;
using Agenda.DataAccess.repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.services
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetContactsDto();
        Task<ContactDto> GetContactById(int id);
        Task DeleteEmailById(int id);
        Task DeletePhoneById(int id);
        Task DeleteContact(int id);
        Task CreateContact(ContactCreationDto contactCreationDto);
        Task UpdateContact(ContactUpdateDto contactUpdateDto);
    }
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;
        private readonly IPhoneRepository phoneRepository;
        private readonly IEmailRepository emailRepository;

        public ContactService(
            IContactRepository contactRepository,
            IPhoneRepository phoneRepository,
            IEmailRepository emailRepository,
            IMapper mapper)
        {
            this.contactRepository = contactRepository;
            this.mapper = mapper;
            this.phoneRepository = phoneRepository;
            this.emailRepository = emailRepository;
        }

        public async Task<ContactDto> GetContactById(int id)
        {
            var contactDb = await contactRepository.GetById(id);
            
            if (contactDb == null) throw new NullReferenceException("contact does not exists");


            var contactDto = new ContactDto()
            {
                Id = id,
                Name = contactDb.Name,
                LastName = contactDb.LastName,
                Direction = contactDb.Direction,
                NationalId = contactDb.NationalId,
                Emails = mapper.Map<List<EmailDto>>(contactDb.Emails),
                Phones = mapper.Map<List<PhoneDto>>(contactDb.Phones)
            };

            return contactDto;

        }

        public async Task<List<ContactDto>> GetContactsDto()
        {
            var contacts = await contactRepository.GetAll();

            var contactsDto = new List<ContactDto>();

            foreach (var contact in contacts)
            {
                contactsDto.Add(new ContactDto
                {
                    Id= contact.Id,
                    Name = contact.Name,
                    LastName = contact.LastName,
                    Direction = contact.Direction,
                    NationalId = contact.NationalId,
                    Phones = mapper.Map<List<PhoneDto>>(contact.Phones),
                    Emails = mapper.Map<List<EmailDto>>(contact.Emails)
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

        }

        public async Task UpdateContact(ContactUpdateDto contactUpdateDto)
        {

            try
            {
                var contact = await contactRepository.GetById(contactUpdateDto.Id);

                if (contact == null)
                    throw new Exception("Not object found with the Id:" + contactUpdateDto.Id);

                contact.Name = contactUpdateDto.Name;
                contact.LastName = contactUpdateDto.LastName;
                contact.Direction = contactUpdateDto.Direction;
                contact.NationalId = contactUpdateDto.NationalId;

                foreach (var phoneDto in contactUpdateDto.Phones)
                {

                    if (phoneDto.Id is not null)
                    {
                        var phoneDb = await phoneRepository.getById(phoneDto.Id);
                        phoneDb.Number = phoneDto.Number;
                    }
                    else
                    {
                        var newPhoneDb = new Phone();
                        newPhoneDb.Number = phoneDto.Number;
                        contact.Phones.Add(newPhoneDb);
                    }

                }

                foreach (var emailDto in contactUpdateDto.Emails)
                {
                    if (emailDto.Id is not null)
                    {
                        var emailDb = await emailRepository.getById(emailDto.Id);
                        emailDb.EmailAddress = emailDto.EmailContact;
                    }
                    else
                    {
                        var newEmailDb = new Email();
                        newEmailDb.EmailAddress = emailDto.EmailContact;
                        contact.Emails.Add(newEmailDb);
                    }
                }

                await contactRepository.SaveChangesAsync();


            }
            catch (Exception )
            {

                throw;
            }

            
        }

        public async Task DeleteContact(int id)
        {
            var contact = await contactRepository.GetById(id);

            contact.Deleted = "true";

            await contactRepository.SaveChangesAsync();
        }

        public async Task DeleteEmailById(int id)
        {
            emailRepository.DeleteById(id);

            await emailRepository.SaveChangesAsync();
        }

        public async Task DeletePhoneById(int id)
        {
            phoneRepository.DeleteById(id);

            await phoneRepository.SaveChangesAsync();
        }

        //External mapping methods
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
