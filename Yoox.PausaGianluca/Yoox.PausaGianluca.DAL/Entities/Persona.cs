using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.PausaGianluca.DAL.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public ICollection<Casa> Case { get; set; }

        public Persona()
        {
            this.Case = new HashSet<Casa>();
        }
    }
}
