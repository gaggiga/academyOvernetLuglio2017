using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Mongo.Entities
{
    public class Incasso
    {
        public ObjectId Id { get; set; }
        public DateTime DataIncasso { get; set; }
        public double Totale { get; set; }

        public Incasso()
        {
            this.DataIncasso = DateTime.UtcNow;
        }
    }
}
