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

        public Tavolo()
        {
            this.Ordini = new HashSet<Ordine>();
            this.DataCreazione = DateTime.UtcNow;
        }
    }
}
