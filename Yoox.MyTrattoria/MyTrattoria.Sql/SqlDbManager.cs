using MyTrattoria.Sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Sql
{
    public class SqlDbManager : IDisposable
    {
        private SqlDbContext db = new SqlDbContext();

        public Tavolo CreaTavolo(string sigla)
        {
            var tavolo = new Tavolo();
            tavolo.Sigla = sigla;
            db.Tavoli.Add(tavolo);
            db.SaveChanges();

            return tavolo;
        }

        public bool ExistsTavolo(string sigla)
        {
            return db.Tavoli.Any(t => t.Sigla == sigla);
        }

        public IEnumerable<Tavolo> GetTavoli()
        {
            return db.Tavoli.Include("Ordini.Comande");
        }


        public Tavolo GetTavolo(int id)
        {
            return db.Tavoli.Include("Ordini.Comande").FirstOrDefault(t => t.Id == id);
        }

        public void RimuoviTavolo(int tavoloId, decimal incassato = 0)
        {
            var tavolo = db.Tavoli.Include("Ordini.Comande").First(t => t.Id == tavoloId);
            
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
                db.Incassi.Add(incasso);
            }

            db.Tavoli.Remove(tavolo);
            db.SaveChanges();
        }

        public Dictionary<string, IEnumerable<Pietanza>> GetMenu()
        {
            return db.Pietanze.GroupBy(p => p.Tipo).ToDictionary(p => p.Key, p => p.AsEnumerable());
        }

        public void Nuova(Pietanza pietanza)
        {
            db.Pietanze.Add(pietanza);
            db.SaveChanges();
        }

        public void Modifica(Pietanza pietanza)
        {
            db.Entry(pietanza).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void EliminaPietanza(int id)
        {
            var pietanza = db.Pietanze.Find(id);
            db.Pietanze.Remove(pietanza);
            db.SaveChanges();
        }

        public Ordine CreaOrdine(int tavoloId)
        {
            var ordine = new Ordine();
            ordine.TavoloId = tavoloId;

            db.Ordini.Add(ordine);
            db.SaveChanges();

            return ordine;
        }

        public Comanda CreaComanda(int ordineId, int pietanzaId)
        {
            var pietanza = db.Pietanze.Find(pietanzaId);
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
