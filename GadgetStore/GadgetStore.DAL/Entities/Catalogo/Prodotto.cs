using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetStore.DAL.Entities.Catalogo
{
    [Table("Prodotti")]
    public class Prodotto
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Nome { get; set; }
        
        public ICollection<Categoria> Categorie { get; set; }
        public ICollection<Articolo> Articoli { get; set; }

        public Prodotto()
        {
            this.Categorie = new HashSet<Categoria>();
            this.Articoli = new HashSet<Articolo>();
        }
    }
}