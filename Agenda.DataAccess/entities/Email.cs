using Agenda.BusinessLogic.entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DataAccess.entities
{
    [Table("Correos")]
    public class Email
    {
        public int Id { get; set; }

        [EmailAddress]
        [Column("correo")]
        public string EmailAddress { get; set; }
        public virtual Contact Contact { get; set; }
        //[Column("personaId")]
        //public int ContactId { get; set; }
    }
}
