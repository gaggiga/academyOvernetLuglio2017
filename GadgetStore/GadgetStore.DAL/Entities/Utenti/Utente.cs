using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Utenti
{
    [Table("Utenti")]
    public abstract class Utente
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Nome { get; set; }
        [Required, StringLength(50)]
        public string Cognome { get; set; }
    }
}
