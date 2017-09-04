using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.CampoCalcolato
{
    [Table("RigheOrdine")]
    public class RigaOrdine : INeedToCalculateSomething
    {
        public int Id { get; set; }
        public string Descrizione { get; set; }

        public decimal Prezzo { get; set; }

        public int Quantità { get; set; }

        public decimal Sconto { get; set; }

        public decimal TotaleRiga { get; set; }

        public Ordine Ordine { get; set; }

        public void CalculateIt()
        {
            Console.WriteLine("RigaOrdine: Chiamato calcola totale");
            this.TotaleRiga = this.Quantità * this.Prezzo - this.Sconto;
        }
    }
}
