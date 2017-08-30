using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Sql.Entities
{
    public class Ordine
    {
        public int Id { get; set; }
        public int TavoloId { get; set; }
        public Tavolo Tavolo { get; set; }
        public ICollection<Comanda> Comande { get; set; }
        public DateTime DataCreazione { get; set; }

        public Ordine()
        {
            this.Comande = new HashSet<Comanda>();
            this.DataCreazione = DateTime.UtcNow;
        }
    }
}
