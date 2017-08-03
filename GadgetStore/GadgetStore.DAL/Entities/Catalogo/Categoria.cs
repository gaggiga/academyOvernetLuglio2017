using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Catalogo
{
    [Table("Categorie")]
    public class Categoria
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Nome { get; set; }
        public int? FamigliaId { get; set; }
        public Famiglia Famiglia { get; set; }
        public int? PadreId { get; set; }
        public Categoria Padre { get; set; }
        public ICollection<Categoria> Figli { get; set; }
        public ICollection<Prodotto> Prodotti { get; set; }

        public Categoria()
        {
            this.Figli = new HashSet<Categoria>();
            this.Prodotti = new HashSet<Prodotto>();
        }
    }
}
