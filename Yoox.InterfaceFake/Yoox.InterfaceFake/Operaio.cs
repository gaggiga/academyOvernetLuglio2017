using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.InterfaceFake
{
    public class Operaio : ILavoro
    {
        public void Lavora()
        {
            Console.WriteLine("Faccio cose, vedo gente");
        }
    }
}
