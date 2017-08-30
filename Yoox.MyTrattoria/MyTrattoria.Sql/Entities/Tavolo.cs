using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Sql.Entities
{
    public class Tavolo
    {
        public int Id { get; set; }
        public ICollection<Ordine> Ordini { get; set; }
        public DateTime DataCreazione { get; set; }

        public Tavolo()
        {
            this.Ordini = new HashSet<Ordine>();
            this.DataCreazione = DateTime.UtcNow;
        }
    }
}
