﻿using System;
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
        public void MoveAll_Should_MoveTheSingleFileContained()
        {
            // Arrange
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile.txt" });

            myFile.Setup(f => f.Move( It.IsAny<string>(), It.IsAny<string>()));
            
            // Act
            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            // Assert
            myFile.Verify(f => f.GetFiles(It.Is<string>(s => s == @"c:\")), Times.Once());
            myFile.Verify(f => f.Move( It.Is<string>(s => s == @"c:\miofile.txt")
                                     , It.Is<string>(s => s == @"d:\miofile.txt")), Times.Once());
        }

        [TestMethod]
        public void MoveAll_Should_MoveAllGivenFilesInDestinationFolder()
        {
            // Arrange
            var myFile = new Mock<IFile>();
            myFile.Setup(f => f.GetFiles(It.IsAny<string>()))
                  .Returns(new string[] { @"c:\miofile1.txt", @"c:\miofile2.txt" , @"c:\miofile3.txt" });

            myFile.Setup(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var filesMover = new FilesMover(myFile.Object);
            filesMover.MoveAll(@"c:\", @"d:\");

            // Assert
            myFile.Verify(f => f.GetFiles(It.Is<string>(s => s == @"c:\")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile1.txt")
                                     , It.Is<string>(s => s == @"d:\miofile1.txt")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile2.txt")
                                     , It.Is<string>(s => s == @"d:\miofile2.txt")), Times.Once());
            myFile.Verify(f => f.Move(It.Is<string>(s => s == @"c:\miofile3.txt")
                                     , It.Is<string>(s => s == @"d:\miofile3.txt")), Times.Once());

        }
    }
}
