using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrattoria.Mongo.Entities
{
    public class Pietanza
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public double Prezzo { get; set; }
    }
}
