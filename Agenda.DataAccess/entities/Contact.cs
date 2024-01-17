using Agenda.DataAccess.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.BusinessLogic.entities
{
    [Table("Contactos")]
    public class Contact
    {
        public int Id { get; set; }
        [Column("nombre")]
        public string Name { get; set; }
        [Column("apellido")]
        public string LastName { get; set; }

        [Column("cedula")]
        public string NationalId { get; set; }
        [Column("direccion")]
        public string Direction { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }

    }
}
