using MongoDB.Bson;
using MongoDB.Driver;
using MyTrattoria.Mongo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTrattoria.Mongo
{
    public class MongoDbManager
    {
        IMongoCollection<Tavolo> tavoli = MongoDbContext.GetCollection<Tavolo>("tavoli");
        IMongoCollection<Incasso> incassi = MongoDbContext.GetCollection<Incasso>("incassi");
        IMongoCollection<Pietanza> pietanze = MongoDbContext.GetCollection<Pietanza>("pietanze");

        public Tavolo CreaTavolo(string sigla)
        {
            var tavolo = new Tavolo();
            tavolo.Sigla = sigla;
            tavoli.InsertOne(tavolo);

            return tavolo;
        }

        public bool ExistsTavolo(string sigla)
        {
            return tavoli.AsQueryable().Any(t => t.Sigla == sigla);
        }

        public IEnumerable<Tavolo> GetTavoli()
        {
            return tavoli.AsQueryable().ToList();
        }
        
        public Tavolo GetTavolo(string id)
        {
            try
            {
                return tavoli.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));
            }
            catch(FormatException)
            {
                return null;
            }
        }

        public void RimuoviTavolo(string tavoloId, double? incassato)
        {
            var tavoli = this.tavoli.AsQueryable().ToList();
            var tavolo = tavoli.FirstOrDefault(x => x.Id.Equals(tavoloId));

            if( tavolo == null )
            {
                return;
            }

            if (incassato == null)
            {
                incassato = tavolo.Ordini
                                  .SelectMany(o => o.Comande.Where(c => c.Stato == StatoComanda.Servita))
                                  .Sum(c => c.Prezzo);
            }

            if(incassato > 0)
            {
                var incasso = new Incasso();
                incasso.Totale = (double)incassato;
                incassi.InsertOne(incasso);
            }

            this.tavoli.DeleteOne( t => t.Id.Equals(tavoloId) );
        }

        public Dictionary<string, List<Pietanza>> GetMenu()
        {
            var pietanze = this.pietanze.AsQueryable().ToList();
            var menu = pietanze.GroupBy( p => p.Tipo, (key, group) => new { tipo = key, pietanze = group.ToList() } )
                               .ToDictionary(g => g.tipo, g => g.pietanze);

            return menu;
        }

        public void NuovaPietanza(string nome, string tipo, double prezzo)
        {
            var pietanza = new Pietanza();
            pietanza.Nome = nome;
            pietanza.Tipo = tipo;
            pietanza.Prezzo = prezzo;

            pietanze.InsertOne(pietanza);
        }

        public bool ModificaPietanza(string pietanzaId, string nome, string tipo, double prezzo)
        {
            Pietanza pietanza;
            try
            {
                var pietanze = this.pietanze.AsQueryable().ToList();
                pietanza = pietanze.FirstOrDefault(p => p.Id.Equals(pietanzaId));
            }
            catch (FormatException)
            {
                return false;
            }

            if (pietanza == null)
            {
                return false;
            }
            
            pietanza.Nome = nome;
            pietanza.Tipo = tipo;
            pietanza.Prezzo = prezzo;

            this.pietanze.ReplaceOne( p => p.Id.Equals(pietanzaId), pietanza );

            return true;
        }
        
        public bool EliminaPietanza(string pietanzaId)
        {
            try
            {
                pietanze.DeleteOne(p => p.Id.Equals(pietanzaId));
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        public Ordine CreaOrdine(string tavoloId, string[] pietanzeId)
        {
            var ordine = new Ordine();
            
            foreach (var pietanzaId in pietanzeId)
            {
                ordine.Comande.Add( this.CreaComanda( pietanzaId ) );
            }
            
            tavoli.UpdateOne(x => x.Id.Equals(tavoloId), Builders<Tavolo>.Update.Push("Ordini", ordine));
            
            return ordine;
        }

        private Comanda CreaComanda(string pietanzaId)
        {
            var pietanza = pietanze.AsQueryable().First( p => p.Id == pietanzaId );
            var comanda = new Comanda();
            comanda.Nome = pietanza.Nome;
            comanda.Prezzo = pietanza.Prezzo;
            comanda.Stato = StatoComanda.Ordinata;

            return comanda;
        }

        public bool ModificaStatoComanda(string comandaId, StatoComanda stato)
        {
            if (!Guid.TryParse(comandaId, out var guid))
            {
                return false;
            }

            // Ricerca comanda su cui eseguire l'operazione
            var tavoli = this.tavoli.AsQueryable().ToList();
            var queryResponce = tavoli.Select(t => new
            {
                tavoloId = t.Id,
                ordine = t.Ordini
                          .Where(o => o.Comande
                                       .Where(c => c.Id.Equals(comandaId)).Count() > 0 )
                          .FirstOrDefault(),

                comanda = t.Ordini
                           .Where(o => o.Comande
                                       .Where(c => c.Id.Equals(comandaId)).Count() > 0)
                           .Select(o => o.Comande
                                         .FirstOrDefault(c => c.Id.Equals(comandaId)) )
                           .FirstOrDefault()
            }).FirstOrDefault( qr => qr.comanda != null );

            if (queryResponce == null )
            {
                return false;
            }

            // Estrazione Dati dalla risposta della query
            Tavolo tavolo = tavoli.Select(t => t)
                               .Where(t => t.Id == queryResponce.tavoloId)
                               .First();
            Ordine ordine = queryResponce.ordine;
            Comanda comanda = queryResponce.comanda;

            // Lavoro sullo stato
            comanda.Stato = stato;

            // Preparazione oggetto da caricare nel database
            var oldComanda = ordine.Comande.FirstOrDefault(c => c.Id.Equals(comanda.Id));
            ordine.Comande.Remove(oldComanda);
            ordine.Comande.Add(comanda);

            var oldOrdine = tavolo.Ordini.FirstOrDefault(o => o.Id.Equals(ordine.Id));
            tavolo.Ordini.Remove(oldOrdine);
            tavolo.Ordini.Add(ordine);

            // Caricamento oggetto modificato nel database
            this.tavoli.ReplaceOne(t => t.Id.Equals(tavolo.Id), tavolo);

            return true;

        }

        public Dictionary<string, IEnumerable<Comanda>> GetComandeDaPreparare()
        {
            var tavoli = this.tavoli.AsQueryable().ToList();
            return tavoli.Select( t => new
                  {
                      id = t.Id,
                      comande = t.Ordini.OrderBy(o => o.DataCreazione)
                                        .SelectMany(o => o.Comande.Where(c => c.Stato == StatoComanda.Ordinata))
                  } )
                  .Where( x => x.comande.Count() > 0 )
                  .ToDictionary(x => x.id, x => x.comande);
        }

        public bool ComandaPronta(string comandaId)
        {
            return ModificaStatoComanda(comandaId, StatoComanda.DaServire);
        }

        public bool ComandaServita(string comandaId)
        {
            return ModificaStatoComanda(comandaId, StatoComanda.Servita);
        }

        public bool ComandaAnnullata(string comandaId)
        {
            return ModificaStatoComanda(comandaId, StatoComanda.Annullata);
        }

    }
}
