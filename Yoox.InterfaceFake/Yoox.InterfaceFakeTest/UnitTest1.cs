using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.InterfaceFake;

namespace Yoox.InterfaceFakeTest
{
    [TestClass]
    public class CaporepartoTest
    {
        [TestMethod]
        public void FaiLavorare_ConDueOperaiPassando3_DeveFarLavorareInTotale6()
        {
            // Arrange
            var myArray = new ILavoro[]
            {
                new LavoroCheConta(), new LavoroCheConta()
            };

            var capo = new Caporeparto(myArray);
            //var capo = new Caporeparto(new Operaio(), new Operaio());

            // Act
            capo.FaiLavorare(3);

            // Assert
            var totale = 0;

            foreach(LavoroCheConta o in myArray)
            {
                totale += o.NumeroChiamate;
            }

            Assert.AreEqual(6, totale);
        }
    }
}
