using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Sql.Entities
{
    [Table("Comande")]
    public class Comanda
    {
        public int Id { get; set; }
        public int OrdineId { get; set; }
        public Ordine Ordine { get; set; }
        public int PietanzaId { get; set; }
        [Required, StringLength(150)]
        public string Nome { get; set; }
        public decimal Prezzo { get; set; }
        public StatoComanda Stato { get; set; }
    }
}
