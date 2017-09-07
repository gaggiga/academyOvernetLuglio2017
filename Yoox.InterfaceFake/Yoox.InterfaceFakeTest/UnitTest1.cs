using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.InterfaceFake;
using Moq;

namespace Yoox.InterfaceFakeTest
{
    [TestClass]
    public class CaporepartoTest
    {
        [TestMethod]
        public void FaiLavorare_ConDueOperaiPassando3_DeveFarLavorareInTotale6()
        {
            // Arrange
            var myMock = new Mock<ILavoro>();
            var conta = 0;

            myMock
                .Setup(o => o.Lavora())
                .Callback(() => conta++);

            var capo = new Caporeparto(myMock.Object, myMock.Object);

            // Act
            capo.FaiLavorare(3);

            // Assert
            Assert.AreEqual(6, conta);
        }
    }
}
