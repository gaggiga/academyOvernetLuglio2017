using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Catalogo
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? FamigliaId { get; set; }
        public Famiglia Famiglia { get; set; }
        public int? PadreId { get; set; }
        public Categoria Padre { get; set; }
        public IEnumerable<Categoria> Figli { get; set; }
        public IEnumerable<Prodotto> Prodotti { get; set; }

        public Categoria()
        {
            this.Figli = new HashSet<Categoria>();
            this.Prodotti = new HashSet<Prodotto>();
        }
    }
}
