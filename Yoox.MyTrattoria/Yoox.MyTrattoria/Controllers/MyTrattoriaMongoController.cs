using MyTrattoria.Mongo;
using System;
using System.Linq;
using System.Web.Http;

namespace Yoox.MyTrattoria.Controllers
{
    public class MyTrattoriaMongoController : ApiController
    {
        MongoDbManager dbManager = new MongoDbManager();

        [HttpPost]
        public IHttpActionResult CreateTavolo(string sigla)
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

        [HttpGet]
        public IHttpActionResult GetElencoTavoli()
        {
            var tavoli = dbManager.GetTavoli().Select(t => new { id = t.Id, sigla = t.Sigla, stato = t.Stato });
            return Ok(tavoli);
        }

        [HttpGet]
        public IHttpActionResult GetTavolo(string tavoloId)
        {
            var tavolo = dbManager.GetTavolo(tavoloId);

            if (tavolo != null)
            {
                return Ok(tavolo);
            }
            return BadRequest("Id Tavolo Errato");
        }

        [HttpDelete]
        public IHttpActionResult DeleteTavolo(string tavoloId)
        {
            dbManager.RimuoviTavolo(tavoloId, null);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetMenu()
        {
            return Ok(dbManager.GetMenu());
        }

        [HttpPost]
        public IHttpActionResult CreateOrdine(string tavoloId, string[] pietanzeID)
        {
            var ordine = dbManager.CreaOrdine(tavoloId, pietanzeID);

            return Ok(new { dataCreazione = ordine.DataCreazione });
        }
        
        [HttpPut]
        public IHttpActionResult AnnullaComanda(string comandaId)
        {
            var inserted = dbManager.ComandaAnnullata(comandaId);

            if (inserted)
            {
                return Ok();
            }
            return BadRequest("Id errato");
        }

        [HttpPut]
        public IHttpActionResult ServiComanda(string comandaId)
        {
            var inserted = dbManager.ComandaServita(comandaId);

            if (inserted)
            {
                return Ok();
            }
            return BadRequest("Id errato");
        }

        [HttpPut]
        public IHttpActionResult ProntaComanda(string comandaId)
        {
            var inserted = dbManager.ComandaPronta(comandaId);

            if (inserted)
            {
                return Ok();
            }
            return BadRequest("Id errato");
        }

        [HttpGet]
        public IHttpActionResult GetComandeDaPreparare()
        {
            return Ok(dbManager.GetComandeDaPreparare());
        }

        [HttpPost]
        public IHttpActionResult Incassa(string tavoloId, double? incasso = null)
        {
            dbManager.RimuoviTavolo(tavoloId, incasso);
            return Ok();
        }
        
        [HttpPost]
        public IHttpActionResult AddPietanza(string nome, string tipo, double prezzo)
        {
            dbManager.NuovaPietanza(nome, tipo, prezzo);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ModificaPietanza(string pietanzaId, string nome, string tipo, double prezzo)
        {
            if (dbManager.ModificaPietanza(pietanzaId, nome, tipo, prezzo))
            {
                return Ok();
            }
            return BadRequest("Id Tavolo Errato");
        }

        [HttpDelete]
        public IHttpActionResult DeletePietanza(string pietanzaId)
        {
            if (dbManager.EliminaPietanza(pietanzaId))
            {
                return Ok();
            }
            return BadRequest("Id Tavolo Errato");
        }
    }
}
