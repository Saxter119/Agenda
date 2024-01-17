using Agenda.DataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.dtos
{
    public class ContactCreationDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public string Direction { get; set; }
        public List<EmailDto> Emails { get; set; }
        public List<PhoneDto> Phones { get; set; }
    }
}
