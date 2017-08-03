using GadgetStore.DAL.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Vendita
{
    [Table("ArticoliNeiCarrelli")]
    public class ArticoloNelCarrello
    {
        // Per convenzione il campo chiamato Id viene considerato
        // automaticamente da Entity Framework:
        // - Primary KEy
        // - NOT NULL
        // - IDENTITY
        public int Id { get; set; }
        public int ArticoloId { get; set; }
        public Articolo Articolo { get; set; }
        [ForeignKey("Carrello")]
        public int CarrelloId { get; set; }
        public Carrello Carrello { get; set; }
        public decimal Quantità { get; set; }
        public decimal Prezzo { get; set; }
    }
}
