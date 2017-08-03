using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Vendita
{
    [Table("Ordini")]
    public class Ordine
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Utenti.Cliente Cliente { get; set; }
        public ICollection<RigaOrdine> RigheOrdine { get; set; }

        public Ordine()
        {
            this.RigheOrdine = new HashSet<RigaOrdine>();
        }
    }
}
