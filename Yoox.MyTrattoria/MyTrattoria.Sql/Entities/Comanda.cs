using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Sql.Entities
{
    public class Comanda
    {
        public int Id { get; set; }
        public int OrdineId { get; set; }
        public Ordine Ordine { get; set; }
        public int PietanzaId { get; set; }
        public string Nome { get; set; }
        public decimal Prezzo { get; set; }
        public StatoComanda Stato { get; set; }
    }
}
