using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyTrattoria.Mongo.Entities
{
    public class Comanda
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public double Prezzo { get; set; }
        [BsonRepresentation(BsonType.String)]
        public StatoComanda Stato { get; set; }

        public Comanda()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
