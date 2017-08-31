﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrattoria.Mongo.Entities
{
    public class Comanda
    {
        public Pietanza Pietanza { get; set; }
        public string Nome { get; set; }
        public double Prezzo { get; set; }
        [BsonRepresentation(BsonType.String)]
        public StatoComanda Stato { get; set; }
    }
}
