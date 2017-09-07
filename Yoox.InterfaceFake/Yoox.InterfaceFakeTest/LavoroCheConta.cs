using System;
using Yoox.InterfaceFake;

namespace Yoox.InterfaceFakeTest
{
    internal class LavoroCheConta : ILavoro
    {
        public int NumeroChiamate { get; internal set; }

        public void Lavora()
        {
            this.NumeroChiamate++;
        }
    }
}