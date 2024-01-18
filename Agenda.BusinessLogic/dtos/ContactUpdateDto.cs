using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.dtos
{
    public class ContactUpdateDto : ContactCreationDto
    {
        public int Id { get; set; }
    }
}
