using MyTrattoria.Sql;
using MyTrattoria.Sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Yoox.MyTrattoria.Controllers
{
    public class MyTrattoriaSqlController : ApiController
    {
        SqlDbManager db = new SqlDbManager();

        public IHttpActionResult CreaTavolo(string sigla)
        {
            if (String.IsNullOrEmpty(sigla))
            {
                return BadRequest("Indicare la sigla del tavolo");
            }

            if(db.ExistsTavolo(sigla))
            {
                return BadRequest("Sigla già usata in un altro tavolo");
            }

            var tavolo = db.CreaTavolo(sigla);

            return Ok(new { id = tavolo.Id });
        }

        public IHttpActionResult ElencoTavoli()
        {
            var tavoli = db.GetTavoli().Select(t => new { id = t.Id, sigla = t.Sigla, stato = t.Stato });
            return Ok(tavoli);
        }

        public IHttpActionResult RimuoviTavolo(int tavoloId)
        {
            db.RimuoviTavolo(tavoloId);
            return Ok();
        }

        public IHttpActionResult CreaOrdine(int tavoloId)
        {
            var ordine = db.CreaOrdine(tavoloId);

            return Ok(new { id = ordine.Id });
        }

        public IHttpActionResult Menu()
        {
            return Ok(db.GetMenu());
        }

        public IHttpActionResult CreaComanda(int ordineId, int pietanzaId)
        {
            var comanda = db.CreaComanda(ordineId, pietanzaId);

            return Ok(new { id = comanda.Id });
        }

        public IHttpActionResult AnnullaComanda(int comandaId)
        {
            db.ComandaAnnullata(comandaId);

            return Ok();
        }

        public IHttpActionResult ServiComanda(int comandaId)
        {
            db.ComandaServita(comandaId);

            return Ok();
        }

        public IHttpActionResult ProntaComanda(int comandaId)
        {
            db.ComandaPronta(comandaId);

            return Ok();
        }
        public IHttpActionResult Incassa(int tavoloId, decimal incasso)
        {
            db.RimuoviTavolo(tavoloId, incasso);
            return Ok();
        }

        public IHttpActionResult GetTavolo(int tavoloId)
        {
            var tavolo = db.GetTavolo(tavoloId);

            return Ok(tavolo);
        }


        public IHttpActionResult AddPietanza(string nome, string tipo, decimal prezzo)
        {
            var pietanza = new Pietanza();
            pietanza.Nome = nome;
            pietanza.Tipo = tipo;
            pietanza.Prezzo = prezzo;
            db.Nuova(pietanza);

            return Ok();
        }

        public IHttpActionResult GetComandeDaPreparare()
        {
            return Ok(db.GetComandeDaPreparare());
        }

    protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
