using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Sql.Entities
{
    [Table("Tavoli")]
    public class Tavolo
    {
        public int Id { get; set; }
        [Required, StringLength(10)]
        public string Sigla { get; set; }
        public ICollection<Ordine> Ordini { get; set; }
        public DateTime DataCreazione { get; set; }

        [NotMapped]
        public StatoTavolo Stato
        {
            get
            {
                // In attesa di ordine: Se ha zero comande in stato ordinata / da servire
                // In attesa di portate: se ha almeno una comanda ordinata
                // Da servire (vince su in attesa di portate): se ha almeno una comanda DaServire
                if(this.Ordini.Any(o => o.Comande.Any(c => c.Stato == StatoComanda.DaServire)))
                {
                    return StatoTavolo.Servire;
                }

                if(this.Ordini.Any(o => o.Comande.Any(c => c.Stato == StatoComanda.Ordinata)))
                {
                    return StatoTavolo.Cucinare;
                }

                return StatoTavolo.PrendereOrdini;
            }
        }

        public Tavolo()
        {
            this.Ordini = new HashSet<Ordine>();
            this.DataCreazione = DateTime.UtcNow;
        }
    }
}
