using Agenda.BusinessLogic.dtos;
using Agenda.BusinessLogic.services;
using AutoMapper;
using FluentValidation;
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


        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var contactDto = await contactService.GetContactById(id);

                return Ok(contactDto);
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

                return Ok(new { message = "Contact created!" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpPut]
        [Route("updateContact")]
        public async Task<IActionResult> Update(ContactUpdateDto contactUpdateDto)
        {
            try
            {
                await contactService.UpdateContact(contactUpdateDto);

                return Ok(new { message = "Contact updated!" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("deleteEmail/{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            try
            {
                await contactService.DeleteEmailById(id);

                return Ok(new { message = "email deleted!" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("deletePhone/{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            try
            {
                await contactService.DeletePhoneById(id);

                return Ok(new { message = "phone deleted!" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("deleteContact/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await contactService.DeleteContact(id);

                return Ok(new { message = "contact deleted!" });
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
