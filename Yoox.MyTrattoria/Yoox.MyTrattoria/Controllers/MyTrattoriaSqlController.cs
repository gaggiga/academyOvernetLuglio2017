using MyTrattoria.Sql;
using System;
using System.Linq;
using System.Web.Http;

namespace Yoox.MyTrattoria.Controllers
{
    public class MyTrattoriaSqlController : ApiController
    {
        SqlDbManager db = new SqlDbManager();

        [HttpPost]
        public IHttpActionResult CreaTavolo(string sigla)
        {
            if (String.IsNullOrEmpty(sigla))
            {
                return BadRequest("Indicare la sigla del tavolo");
            }

            if (db.ExistsTavolo(sigla))
            {
                return BadRequest("Sigla già usata in un altro tavolo");
            }

            var tavolo = db.CreaTavolo(sigla);

            return Ok(new { id = tavolo.Id });
        }

        [HttpGet]
        public IHttpActionResult GetElencoTavoli()
        {
            var tavoli = db.GetTavoli().Select(t => new { id = t.Id, sigla = t.Sigla, stato = t.Stato });
            return Ok(tavoli);
        }

        [HttpGet]
        public IHttpActionResult GetTavolo(int tavoloId)
        {
            var tavolo = db.GetTavolo(tavoloId);

            return Ok(tavolo);
        }

        [HttpDelete]
        public IHttpActionResult DeleteTavolo(int tavoloId)
        {
            db.RimuoviTavolo(tavoloId);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetMenu()
        {
            return Ok(db.GetMenu());
        }

        [HttpPost]
        public IHttpActionResult CreateOrdine(int tavoloId)
        {
            var ordine = db.CreaOrdine(tavoloId);

            return Ok(new { id = ordine.Id });
        }
        
        [HttpPost]
        public IHttpActionResult CreateComanda(int ordineId, int pietanzaId)
        {
            var comanda = db.CreaComanda(ordineId, pietanzaId);

            return Ok(new { id = comanda.Id });
        }

        [HttpPut]
        public IHttpActionResult AnnullaComanda(int comandaId)
        {
            db.ComandaAnnullata(comandaId);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ServiComanda(int comandaId)
        {
            db.ComandaServita(comandaId);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ProntaComanda(int comandaId)
        {
            db.ComandaPronta(comandaId);

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetComandeDaPreparare()
        {
            return Ok(db.GetComandeDaPreparare());
        }

        [HttpPost]
        public IHttpActionResult Incassa(int tavoloId, decimal incasso)
        {
            db.RimuoviTavolo(tavoloId, incasso);
            return Ok();
        }
        
        [HttpPost]
        public IHttpActionResult AddPietanza(string nome, string tipo, decimal prezzo)
        {
            db.NuovaPietanza(nome, tipo, prezzo);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ModificaPietanza(int pietanzaId, string nome, string tipo, decimal prezzo)
        {
            db.ModificaPietanza(pietanzaId, nome, tipo, prezzo);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeletePietanza(int pietanzaId)
        {
            db.EliminaPietanza(pietanzaId);
            return Ok();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}