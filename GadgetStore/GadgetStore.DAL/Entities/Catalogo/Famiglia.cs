using System.Collections.Generic;

namespace GadgetStore.DAL.Entities.Catalogo
{
    public class Famiglia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Categoria> Categorie { get; set; }

        public Famiglia()
        {
            this.Categorie = new HashSet<Categoria>();
        }
    }
}