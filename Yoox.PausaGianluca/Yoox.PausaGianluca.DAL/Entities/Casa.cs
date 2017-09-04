using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.PausaGianluca.DAL.Entities
{
    public class Casa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Persona> Proprietari { get; set; }

        public Casa()
        {
            this.Proprietari = new HashSet<Persona>();
        }
    }
}
