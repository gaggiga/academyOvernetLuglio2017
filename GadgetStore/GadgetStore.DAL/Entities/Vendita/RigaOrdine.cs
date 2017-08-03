namespace GadgetStore.DAL.Entities.Vendita
{
    public class RigaOrdine
    {
        public int Id { get; set; }
        public int ArticoloId { get; set; }
        public string NomeProdotto { get; set; }
        public decimal Quantità { get; set; }
        public decimal Prezzo { get; set; }
        public int OrdineId { get; set; }
        public Ordine Ordine { get; set; }
    }
}