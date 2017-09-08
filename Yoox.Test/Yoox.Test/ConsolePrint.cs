using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.Test
{
    public class ConsolePrint : IPrint
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
