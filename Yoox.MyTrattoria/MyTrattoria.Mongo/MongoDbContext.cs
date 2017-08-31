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


        //public SqlDbContext()
        //    : base("name=SqlDbConnectionString")
        //{
        //}

        //public virtual DbSet<Pietanza> Pietanze { get; set; }
        //public virtual DbSet<Incasso> Incassi { get; set; }
        //public virtual DbSet<Tavolo> Tavoli { get; set; }
        //public virtual DbSet<Ordine> Ordini { get; set; }
        //public virtual DbSet<Comanda> Comande { get; set; }
    }

    
}