using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Utenti
{
    [Table("Clienti")]
    public class Cliente : Utente
    {
        public Vendita.Carrello Carrello { get; set; }

        public Cliente()
        {
            this.Carrello = new Vendita.Carrello();
        }
    }
}
