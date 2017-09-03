using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Mongo.Entities
{
    public class Ordine
    {
        public string Id { get; set; }
        public ICollection<Comanda> Comande { get; set; }
        public DateTime DataCreazione { get; set; }

        public Ordine()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comande = new HashSet<Comanda>();
            this.DataCreazione = DateTime.UtcNow;
        }
    }
}
