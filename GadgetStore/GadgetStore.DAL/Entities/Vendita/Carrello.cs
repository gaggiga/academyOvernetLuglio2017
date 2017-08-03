using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Vendita
{
    [Table("Carrelli")]
    public class Carrello
    {
        [Key()]
        public int ClienteId { get; set; }
        public Utenti.Cliente Cliente { get; set; }
        public DateTime DataCreazione { get; set; }

        public ICollection<ArticoloNelCarrello> Articoli { get; set; }

        public Carrello()
        {
            this.Articoli = new HashSet<ArticoloNelCarrello>();
            this.DataCreazione = DateTime.UtcNow;
        }
    }
}
