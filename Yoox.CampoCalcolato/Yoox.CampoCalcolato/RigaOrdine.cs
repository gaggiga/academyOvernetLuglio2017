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
    public class RigaOrdine
    {
        private decimal? totaleRiga = null;
        private decimal prezzo { get; set; }
        internal static readonly Expression<Func<RigaOrdine, decimal>> PrezzoExpression = p => p.prezzo;

        private int quantità { get; set; }
        internal static readonly Expression<Func<RigaOrdine, int>> QuantitàExpression = p => p.quantità;

        private decimal sconto { get; set; }
        internal static readonly Expression<Func<RigaOrdine, decimal>> ScontoExpression = p => p.sconto;

        public int Id { get; set; }
        public string Descrizione { get; set; }

        [NotMapped]
        public decimal Prezzo
        {
            get { return prezzo; }
            set { prezzo = value; RicalcolaTotaleRiga(); }
        }

        [NotMapped]
        public int Quantità
        {
            get { return quantità; }
            set { quantità = value; RicalcolaTotaleRiga(); }
        }


        [NotMapped]
        public decimal Sconto
        {
            get { return sconto; }
            set { sconto = value; RicalcolaTotaleRiga(); }
        }
        
        public decimal TotaleRiga
        {
            get
            {
                if(totaleRiga == null)
                {
                    RicalcolaTotaleRiga();
                }

                return totaleRiga.Value;
            }

            set
            {
                this.totaleRiga = value;
            }
        }

        public Ordine Ordine { get; set; }

        private void RicalcolaTotaleRiga()
        {
            Console.WriteLine("RigaOrdine: Chiamato calcola totale");
            this.TotaleRiga = this.Quantità * this.Prezzo - this.Sconto;
        }
    }
}
