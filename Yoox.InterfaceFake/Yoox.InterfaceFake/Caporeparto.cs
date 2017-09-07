using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.InterfaceFake
{
    public class Caporeparto
    {
        public Caporeparto(params ILavoro[] operai)
        {
            Operai = operai;
        }

        public void FaiLavorare(int quanto)
        {
            foreach(var o in this.Operai)
            {
                for(var i = 0; i < quanto; i++)
                {
                    o.Lavora();
                }
            }
        }

        public ILavoro[] Operai { get; private set; }
    }
}
