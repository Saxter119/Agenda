using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.dtos
{
    public class ContactDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public string Direction { get; set; }
        public List<EmailDto> Email { get; set; }
        public List<PhoneDto> Phone { get; set; }

    }
}
