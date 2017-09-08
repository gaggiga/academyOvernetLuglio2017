using System;
using System.IO;
using Yoox.Test;

namespace Yoox.Test
{
    public class FilesMover
    {
        private IFile myFile;

        public FilesMover()
        {
            myFile = new FileSystem();
        }

        public FilesMover(IFile myFile)
        {
            this.myFile = myFile;
        }

        public void MoveAll(string sourcePath, string destinationPath)
        {
            var files = myFile.GetFiles(sourcePath);

            if (files == null) return;

            foreach(var file in files)
            {
                var destination = Path.Combine(destinationPath, Path.GetFileName(file));
                myFile.Move(file, destination);
            }
        }
    }

    public class FileSystem : IFile
    {
        public string[] GetFiles(string path)
        {
            return System.IO.Directory.GetFiles(path);
        }

        public void Move(string fileSource, string fileDestination)
        {
            System.IO.File.Move(fileSource, fileDestination);
        }
    }
}