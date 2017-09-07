using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yoox.InterfaceFake;

namespace Yoox.InterfaceFakeTest
{
    
    public class XUnitTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(8)]
        public void FaiLavorare_ConDueOperaiPassando_n_DeveFarLavorareInTotale_nPer2(int n)
        {
            // Arrange
            var myMock = new Mock<ILavoro>();
            myMock.Setup(o => o.Lavora());

            var capo = new Caporeparto(myMock.Object, myMock.Object);

            // Act
            capo.FaiLavorare(n);

            // Assert
            myMock.Verify(o => o.Lavora(), Times.Exactly(n * 2));
        }
    }
}
