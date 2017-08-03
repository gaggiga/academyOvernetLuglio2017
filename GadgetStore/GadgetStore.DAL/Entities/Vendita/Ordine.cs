using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Vendita
{
    public class Ordine
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Utenti.Cliente Cliente { get; set; }
        public IEnumerable<RigaOrdine> RigheOrdine { get; set; }

        public Ordine()
        {
            this.RigheOrdine = new HashSet<RigaOrdine>();
        }
    }
}
