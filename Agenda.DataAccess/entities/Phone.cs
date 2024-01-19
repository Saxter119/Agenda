using Agenda.BusinessLogic.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DataAccess.entities
{
    [Table("Telefonos")]
    public class Phone
    {
        public int Id { get; set; }
        [Column("telefono")]
        public string Number { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
