using MyTrattoria.Mongo;
using MyTrattoria.Mongo.Entities;
using System;
using System.Linq;
using System.Web.Http;

namespace Yoox.MyTrattoria.Controllers
{
    public class MyTrattoriaMongoController : ApiController
    {
        MongoDbManager dbManager = new MongoDbManager();

        public IHttpActionResult CreaTavolo(string sigla)
        {
            if (String.IsNullOrEmpty(sigla))
            {
                return BadRequest("Indicare la sigla del tavolo");
            }

            if (dbManager.ExistsTavolo(sigla))
            {
                return BadRequest("Sigla già usata in un altro tavolo");
            }

            var tavolo = dbManager.CreaTavolo(sigla);

            return Ok(new { id = tavolo.Id });
        }

        public IHttpActionResult ElencoTavoli()
        {
            var tavoli = dbManager.GetTavoli().Select(t => new { id = t.Id, sigla = t.Sigla, stato = t.Stato });
            return Ok(tavoli);
        }

        public IHttpActionResult RimuoviTavolo(string tavoloId)
        {
            dbManager.RimuoviTavolo(tavoloId);
            return Ok();
        }

        public IHttpActionResult CreaOrdine(string tavoloId, string[] pietanzeID)
        {
            var ordine = dbManager.CreaOrdine(tavoloId, pietanzeID);

            return Ok(new { dataCreazione = ordine.DataCreazione });
        }

        public IHttpActionResult Menu()
        {
            return Ok(dbManager.GetMenu());
        }

        public IHttpActionResult AnnullaComanda(string comandaId)
        {
            dbManager.ComandaAnnullata(comandaId);

            return Ok();
        }

        public IHttpActionResult ServiComanda(string comandaId)
        {
            dbManager.ComandaServita(comandaId);

            return Ok();
        }

        public IHttpActionResult ProntaComanda(string comandaId)
        {
            dbManager.ComandaPronta(comandaId);

            return Ok();
        }
        public IHttpActionResult Incassa(string tavoloId, double incasso)
        {
            dbManager.RimuoviTavolo(tavoloId, incasso);
            return Ok();
        }

        public IHttpActionResult GetTavolo(string tavoloId)
        {
            var tavolo = dbManager.GetTavolo(tavoloId);

            return Ok(tavolo);
        }


        public IHttpActionResult AddPietanza(string nome, string tipo, double prezzo)
        {
            var pietanza = new Pietanza();
            pietanza.Nome = nome;
            pietanza.Tipo = tipo;
            pietanza.Prezzo = prezzo;
            dbManager.Nuova(pietanza);

            return Ok();
        }

        public IHttpActionResult GetComandeDaPreparare()
        {
            return Ok(dbManager.GetComandeDaPreparare());
        }
    }
}
