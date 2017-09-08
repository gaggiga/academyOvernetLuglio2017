using System;
using System.IO;
using Yoox.Test;

namespace Yoox.Test
{
    public class FilesMover
    {
        private IFile myFile;
        private IPrint myConsole;

        public FilesMover(IFile myFile = null, IPrint myConsole = null)
        {
            if(myFile == null)
                myFile = new FileSystem();

            if (myConsole == null)
                myConsole = new ConsolePrint();

            this.myFile = myFile;
            this.myConsole = myConsole;
        }

        public void MoveAll(string sourcePath, string destinationPath)
        {
            var files = myFile.GetFiles(sourcePath);

            if (files == null) return;

            foreach(var file in files)
            {
                var f = new MyFile(myFile) { FilePath = file };
                
                var destination = Path.Combine(destinationPath, f.DestinationPath);
                myFile.Move(f.FilePath, destination);

                var folder = Path.Combine(destinationPath, f.DestinationFolder);
                var message = $"Il file {f.FileName} è stato spostato da {sourcePath} a {folder}";
                myConsole.Print(message);
            }
        }

    }    

}