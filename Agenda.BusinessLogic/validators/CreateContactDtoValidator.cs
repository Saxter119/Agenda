using Agenda.BusinessLogic.dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.validators
{
    public class CreateContactDtoValidator : AbstractValidator<ContactCreationDto>
    {
        public CreateContactDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");

            //RuleFor(x => x.Emails)
            //.Must(emailList => emailList != null && emailList.Any(email => !String.IsNullOrEmpty(email.EmailContact)))
            //.WithMessage("Debes enviar al menos un correo o número de contacto");

            //RuleFor(x => x.Phones).
            //Must(phoneList => phoneList != null && phoneList.Any(phone=> !String.IsNullOrEmpty(phone.Number)))
            //    .WithMessage("Debes enviar al menos un correo o número de contacto");


            RuleFor(x => new { x.Emails, x.Phones })
                .Must(list => list.Emails != null || list.Phones != null
                && list.Emails.Any(email => !String.IsNullOrEmpty(email.EmailContact)) 
                || list.Phones.Any(phone=> !string.IsNullOrEmpty(phone.Number)))
                .WithMessage("Al menos un email o número de contacto debe ser enviado");
        }
    }
}
