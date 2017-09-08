using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Yoox.Test;

namespace Yoox.TestTest
{
    [TestClass]
    public class FilesMoverTest
    {
        [TestMethod]
        public void MoveAll_Should_CallGetFilesAtLeastOnce_When_Called()
        {
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()));

            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            myFile.Verify(f => f.GetFiles(It.IsAny<string>()), Times.AtLeastOnce());
        }

        // Obsoleto
        [TestMethod, Ignore]
        public void MoveAll_Should_CallMoveAtLeastOnce_When_Called()
        {
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile.txt" });

            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            myFile.Verify(f => f.Move(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce());      
        }

        [TestMethod]
        public void MoveAll_Should_MoveTheSingleNotValidFileContainedInnotValidSubfolder()
        {
            // Arrange
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile.txt" });
            myFile.Setup(f => f.GetFileSize(It.IsAny<string>())).Returns(1024);

            myFile.Setup(f => f.Move( It.IsAny<string>(), It.IsAny<string>()));
            
            // Act
            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            // Assert
            myFile.Verify(f => f.GetFiles(It.Is<string>(s => s == @"c:\")), Times.Once());
            myFile.Verify(f => f.Move( It.Is<string>(s => s == @"c:\miofile.txt")
                                     , It.Is<string>(s => s == @"d:\notValid\miofile.txt")), Times.Once());
        }

        [TestMethod]
        public void MoveAll_Should_MoveAllGivenNotValidFilesNotValidSubFolder()
        {
            // Arrange
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile1.txt", @"c:\miofile2.txt" , @"c:\miofile3.txt" });
            myFile.Setup(f => f.GetFileSize(It.IsAny<string>())).Returns(1024);

            myFile.Setup(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            // Assert
            myFile.Verify(f => f.GetFiles(It.Is<string>(s => s == @"c:\")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile1.txt")
                                     , It.Is<string>(s => s == @"d:\notValid\miofile1.txt")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile2.txt")
                                     , It.Is<string>(s => s == @"d:\notValid\miofile2.txt")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile3.txt")
                                     , It.Is<string>(s => s == @"d:\notValid\miofile3.txt")), Times.Once());

        }

        [TestMethod]
        public void MoveAll_Should_MoveAllSmallGifInIsValidSmallPicturesSubFolder()
        {
            // Arrange
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile1.gif", @"c:\miofile2.gif" });
            myFile.Setup(f => f.GetFileSize(It.IsAny<string>())).Returns(1024);

            myFile.Setup(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            // Assert
            myFile.Verify(f => f.GetFiles(It.Is<string>(s => s == @"c:\")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile1.gif")
                                     , It.Is<string>(s => s == @"d:\isValid\smallPictures\miofile1.gif")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile2.gif")
                                     , It.Is<string>(s => s == @"d:\isValid\smallPictures\miofile2.gif")), Times.Once());

        }

        [TestMethod]
        public void MoveAll_Should_MoveAllBigGifInIsValidBigPicturesSubFolder()
        {
            // Arrange
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile1.gif", @"c:\miofile2.gif" });
            myFile.Setup(f => f.GetFileSize(It.IsAny<string>())).Returns(1025);

            myFile.Setup(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            // Assert
            myFile.Verify(f => f.GetFiles(It.Is<string>(s => s == @"c:\")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile1.gif")
                                     , It.Is<string>(s => s == @"d:\isValid\bigPictures\miofile1.gif")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile2.gif")
                                     , It.Is<string>(s => s == @"d:\isValid\bigPictures\miofile2.gif")), Times.Once());

        }

        [TestMethod]
        public void MoveAll_Should_MoveAllBigFilesNotPicturesInIsValidBigFilesSubFolder()
        {
            // Arrange
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile1.txt", @"c:\miofile2.txt", @"c:\miofile3.txt" });
            myFile.Setup(f => f.GetFileSize(It.IsAny<string>())).Returns(1025);

            myFile.Setup(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            // Assert
            myFile.Verify(f => f.GetFiles(It.Is<string>(s => s == @"c:\")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile1.txt")
                                     , It.Is<string>(s => s == @"d:\isValid\bigFiles\miofile1.txt")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile2.txt")
                                     , It.Is<string>(s => s == @"d:\isValid\bigFiles\miofile2.txt")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile3.txt")
                                     , It.Is<string>(s => s == @"d:\isValid\bigFiles\miofile3.txt")), Times.Once());

        }

        [TestMethod]
        public void MoveAll_Should_WriteADescriptionLineForEveryFileMoved()
        {
            // Arrange
            var myConsole = new Mock<IPrint>();
            myConsole.Setup(s => s.Print(It.IsAny<string>()));

            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile1.txt", @"c:\miofile2.txt", @"c:\miofile3.gif" });

            myFile.Setup(f => f.GetFileSize(It.IsIn<string>(@"c:\miofile1.txt", @"c:\miofile3.gif")))
                  .Returns(1024);
            myFile.Setup(f => f.GetFileSize(It.Is<string>(s => s == @"c:\miofile2.txt")))
                  .Returns(1025);

            myFile.Setup(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var filesMover = new FilesMover(myFile.Object, myConsole.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            // Assert
            myConsole.Verify(c => c.Print(It.Is<string>(s => s == @"Il file miofile1.txt è stato spostato da c:\ a d:\notValid")), Times.Once());
            myConsole.Verify(c => c.Print(It.Is<string>(s => s == @"Il file miofile2.txt è stato spostato da c:\ a d:\isValid\bigFiles")), Times.Once());
            myConsole.Verify(c => c.Print(It.Is<string>(s => s == @"Il file miofile3.gif è stato spostato da c:\ a d:\isValid\smallPictures")), Times.Once());
        }
    }
}
