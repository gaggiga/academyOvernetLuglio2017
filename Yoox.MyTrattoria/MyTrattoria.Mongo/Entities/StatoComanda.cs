using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Mongo.Entities
{
    public enum StatoComanda : byte
    {
        Ordinata,
        DaServire,
        Servita,
        Annullata,
    }
}
