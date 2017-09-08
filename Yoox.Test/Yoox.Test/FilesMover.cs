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
            var isValidPath = Path.Combine(destinationPath, "isValid");
            var files = myFile.GetFiles(sourcePath);

            if (files == null) return;

            foreach(var file in files)
            {
                var destination = IsValid(file) ? isValidPath : destinationPath;
                destination = Path.Combine(destination, Path.GetFileName(file));
                myFile.Move(file, destination);
            }
        }

        private bool IsValid(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".gif", StringComparison.InvariantCultureIgnoreCase)
                || this.myFile.GetFileSize(filePath) > 1024;
        }
    }    
}