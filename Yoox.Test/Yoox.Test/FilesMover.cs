using System;
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

        public void MoveAll(string v1, string v2)
        {
            var files = myFile.GetFiles(v1);

            if (files == null) return;

            foreach(var file in files)
            {
                var destination = @"d:\miofile.txt";
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