using System.Collections.Generic;

namespace GadgetStore.DAL.Entities.Catalogo
{
    public class Prodotto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public IEnumerable<Categoria> Categorie { get; set; }
        public IEnumerable<Articolo> Articoli { get; set; }

        public Prodotto()
        {
            this.Categorie = new HashSet<Categoria>();
            this.Articoli = new HashSet<Articolo>();
        }
    }
}