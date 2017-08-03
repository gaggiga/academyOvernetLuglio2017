using GadgetStore.DAL.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Vendita
{
    public class ArticoloNelCarrello
    {
        public int ArticoloId { get; set; }
        public Articolo Articolo { get; set; }
        public int CarrelloId { get; set; }
        public Carrello Carrello { get; set; }
        public decimal Quantità { get; set; }
        public decimal Prezzo { get; set; }
    }
}
