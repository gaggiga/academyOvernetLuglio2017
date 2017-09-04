using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTrattoria.Mongo.Entities
{
    public class Tavolo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Sigla { get; set; }
        public ICollection<Ordine> Ordini { get; set; }
        public DateTime DataCreazione { get; set; }

        [BsonIgnore]
        public StatoTavolo Stato
        {
            get
            {
                // In attesa di ordine: Se ha zero comande in stato ordinata / da servire
                // In attesa di portate: se ha almeno una comanda ordinata
                // Da servire (vince su in attesa di portate): se ha almeno una comanda DaServire
                if(this.Ordini.Any(o => o.Comande.Any(c => c.Stato == StatoComanda.DaServire)))
                {
                    return StatoTavolo.Servire;
                }

                if(this.Ordini.Any(o => o.Comande.Any(c => c.Stato == StatoComanda.Ordinata)))
                {
                    return StatoTavolo.Cucinare;
                }

                return StatoTavolo.PrendereOrdini;
            }
        }

        public Tavolo()
        {
            this.Ordini = new HashSet<Ordine>();
            this.DataCreazione = DateTime.UtcNow;
        }
    }
}
