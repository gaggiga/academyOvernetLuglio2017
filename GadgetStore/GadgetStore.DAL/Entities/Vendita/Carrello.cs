using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Vendita
{
    public class Carrello
    {
        public int ClienteId { get; set; }
        public Utenti.Cliente Cliente { get; set; }
        public DateTime DataCreazione { get; set; }

        public IEnumerable<ArticoloNelCarrello> Articoli { get; set; }

        public Carrello()
        {
            this.Articoli = new HashSet<ArticoloNelCarrello>();
            this.DataCreazione = DateTime.UtcNow;
        }
    }
}
