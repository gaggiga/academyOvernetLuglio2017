namespace MyTrattoria.Mongo
{
    using MongoDB.Driver;

    public class MongoDbContext
    {
        public static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var client = new MongoClient("mongodb://localhost:27017/mongoTrattoria");
            var db = client.GetDatabase("mongoTrattoria");

            return db.GetCollection<T>(collectionName);
        }
    }
}