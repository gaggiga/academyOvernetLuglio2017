using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetStore.DAL.Entities.Catalogo
{
    [Table("Famiglie")]
    public class Famiglia
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Nome { get; set; }
        public ICollection<Categoria> Categorie { get; set; }

        public Famiglia()
        {
            this.Categorie = new HashSet<Categoria>();
        }
    }
}