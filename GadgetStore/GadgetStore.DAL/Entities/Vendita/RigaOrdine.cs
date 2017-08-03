using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetStore.DAL.Entities.Vendita
{
    [Table("RigheOrdini")]
    public class RigaOrdine
    {
        public int Id { get; set; }
        public int ArticoloId { get; set; }
        [Required, StringLength(255)]
        public string NomeProdotto { get; set; }
        public decimal Quantità { get; set; }
        public decimal Prezzo { get; set; }
        public int OrdineId { get; set; }
        public Ordine Ordine { get; set; }
    }
}