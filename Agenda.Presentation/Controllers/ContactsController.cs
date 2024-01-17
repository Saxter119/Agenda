using Agenda.BusinessLogic.dtos;
using Agenda.BusinessLogic.services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Presentation.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var contactsDto = await contactService.GetContactsDto();

                return Ok(contactsDto);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Post(ContactCreationDto contactCreationDto)
        {
            try
            {
                await contactService.CreateContact(contactCreationDto);

                return Ok(new { message = "Contact created" } );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

    }
}
