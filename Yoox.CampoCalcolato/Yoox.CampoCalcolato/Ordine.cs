using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.CampoCalcolato
{
    [Table("Ordini")]
    public class Ordine : INeedToCalculateSomething
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public ICollection<RigaOrdine> RigheOrdine { get; set; }
        public decimal TotaleOrdine { get; set; }

        public Ordine()
        {
            this.Data = DateTime.UtcNow;
            this.RigheOrdine = new HashSet<RigaOrdine>();
        }

        public void CalculateIt()
        {
            Console.WriteLine("Ordine:Chiamato calcola totale");
            this.TotaleOrdine = this.RigheOrdine.Sum(r => r.TotaleRiga);
        }
    }
}
