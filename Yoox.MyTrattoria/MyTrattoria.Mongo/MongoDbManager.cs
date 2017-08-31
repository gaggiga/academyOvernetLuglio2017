﻿using MongoDB.Bson;
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

        public Dictionary<string, List<Pietanza>> GetMenu()
        {
            return this.pietanze.AsQueryable()
                           .GroupBy(p => p.Tipo)
                           .ToDictionary(p => p.Key, p => p.ToList());

            // TODO: Non funziona Come si deve, restituisce un array vuoto al posto della lista
            
            // Nuovo tentativo
            //var collection = tavoli.Aggregate().Group(new BsonDocument { "_id", "$tipo" }, );

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
            var pietanza = pietanze.Find(pietanzaId).First();
            var comanda = new Comanda();
            comanda.Nome = pietanza.Nome;
            comanda.Prezzo = pietanza.Prezzo;
            comanda.Stato = StatoComanda.Ordinata;

            return comanda;
        }

        public void ModificaStatoComanda(string comandaId, StatoComanda stato)
        {
            Tavolo tavolo = tavoli.AsQueryable().First(t => t.Ordini.AsQueryable().First(o => o.Comande.AsQueryable().First(c => c.Id.Equals(comandaId)) != null) != null);
            Ordine ordine = tavolo.Ordini.AsQueryable().First(o => o.Comande.AsQueryable().First(c => c.Id.Equals(comandaId)) != null);
            Comanda comanda = ordine.Comande.First(c => c.Id.Equals(comandaId));
            comanda.Stato = stato;
        }

        public Dictionary<string, IEnumerable<Comanda>> GetComandeDaPreparare()
        {
            return tavoli.AsQueryable()
                  .Select(t => new
                  {
                      id = t.Id,
                      comande = t.Ordini.OrderBy(o => o.DataCreazione)
                                        .SelectMany(o => o.Comande.Where(c => c.Stato == StatoComanda.Ordinata))
                  } )
                  .Where( x => x.comande.Count() > 0 )
                  .ToDictionary(x => x.id, x => x.comande);
        }

        public void ComandaPronta(string comandaId)
        {
            ModificaStatoComanda(comandaId, StatoComanda.DaServire);
        }

        public void ComandaServita(string comandaId)
        {
            ModificaStatoComanda(comandaId, StatoComanda.Servita);
        }

        public void ComandaAnnullata(string comandaId)
        {
            ModificaStatoComanda(comandaId, StatoComanda.Annullata);
        }

    }
}
