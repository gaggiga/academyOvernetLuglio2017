using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetStore.DAL.Entities.Catalogo
{
    public class Articolo
    {
        public int Id { get; set; }
        public int ProdottoId { get; set; }
        public Prodotto Prodotto { get; set; }
        public decimal PrezzoListino { get; set; }

        //public Articolo()
        //{
        //    var a = new Articolo();
        //    //var prodotto = db.Prodotti.Find(a.ProdottoId);
        //    //var nome = prodotto.Nome;
        //    var nome = a.Prodotto.Nome;

        //    //var id = this.Prodotto.Id; // SELECT Prodotto.Id FROM Articolo JOIN Prodotto ON Articolo.ProdottoId = Prodotto.Id
        //    //var id = this.ProdottoId;  // SELECT ProdottoId FROM Articolo
        //}
    }


}
