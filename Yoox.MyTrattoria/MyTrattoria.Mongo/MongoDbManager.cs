using MongoDB.Driver;
using MyTrattoria.Mongo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTrattoria.Mongo
{
    public class MongoDbManager : IDisposable
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
            return tavoli.AsQueryable().First( x => x.Id.Equals(id) );
        }

        public void RimuoviTavolo(string tavoloId, double incassato = 0)
        {
            var tavolo = tavoli.AsQueryable().First( x => x.Id.Equals(tavoloId) );
            
            if(incassato == 0)
            {
                incassato = tavolo.Ordini
                                  .SelectMany(o => o.Comande.Where(c => c.Stato == StatoComanda.Servita))
                                  .Sum(c => c.Prezzo);
            }

            if(incassato > 0)
            {
                var incasso = new Incasso();
                incasso.Totale = incassato;
                incassi.InsertOne(incasso);
            }

            tavoli.DeleteOne( t => t.Id.Equals(tavoloId) );
        }

        public Dictionary<string, IEnumerable<Pietanza>> GetMenu()
        {
            return pietanze.AsQueryable()
                           .GroupBy(p => p.Tipo)
                           .ToDictionary(p => p.Key, p => p.AsEnumerable());
        }

        public void Nuova(Pietanza pietanza)
        {
            pietanze.InsertOne(pietanza);
        }

        public void Modifica(Pietanza pietanza)
        {
            pietanze.ReplaceOne( p => p.Id.Equals( pietanza.Id ), pietanza );
        }

        public void EliminaPietanza(string pietanzaId)
        {
            pietanze.DeleteOne( p => p.Id.Equals(pietanzaId) );
        }

        //public Ordine CreaOrdine(string tavoloId)
        //{
        //    var ordine = new Ordine();
            

        //    tavoli.UpdateOne( x => x.Id.Equals(tavoloId), Builders<Tavolo>.Update.Push( "Ordini", ordine ) );

            
        //    return ordine;
        //}

        public Comanda CreaComanda(int ordineId, int pietanzaId)
        {
            var pietanza = pietanze.Find(pietanzaId);
            var comanda = new Comanda();
            comanda.OrdineId = ordineId;
            comanda.PietanzaId = pietanzaId;
            comanda.Nome = pietanza.Nome;
            comanda.Prezzo = pietanza.Prezzo;
            comanda.Stato = StatoComanda.Ordinata;

            db.Comande.Add(comanda);
            db.SaveChanges();

            return comanda;
        }

        public void ModificaStatoComanda(int comandaId, StatoComanda stato)
        {
            var comanda = db.Comande.Find(comandaId);
            comanda.Stato = stato;
            db.SaveChanges();
        }

        public Dictionary<int, IEnumerable<Comanda>> GetComandeDaPreparare()
        {
            return db.Ordini.Include("Comande")
                     .Where(o => o.Comande.Any(c => c.Stato == StatoComanda.Ordinata))
                     .OrderBy(o => o.DataCreazione)
                     .ToDictionary(o => o.Id, o => o.Comande.Where(c => c.Stato == StatoComanda.Ordinata));
        }

        public void ComandaPronta(int comandaId)
        {
            ModificaStatoComanda(comandaId, StatoComanda.DaServire);
        }

        public void ComandaServita(int comandaId)
        {
            ModificaStatoComanda(comandaId, StatoComanda.Servita);
        }

        public void ComandaAnnullata(int comandaId)
        {
            ModificaStatoComanda(comandaId, StatoComanda.Annullata);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.db.Dispose();
                }

                this.db = null;
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

    }
}
