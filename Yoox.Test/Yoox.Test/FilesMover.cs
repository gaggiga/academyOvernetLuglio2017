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
                var f = new MyFile(myFile) { FilePath = file };
                
                var destination = Path.Combine(destinationPath, f.DestinationPath);
                myFile.Move(f.FilePath, destination);
            }
        }

    }    

    public class MyFile
    {
        private IFile myFile;
        public string FilePath { get; set; }

        public MyFile(IFile myFile)
        {
            this.myFile = myFile;
        }

        public string FileName
        {
            get
            {
                return Path.GetFileName(FilePath);
            }
        }

        public bool IsPicture
        {
            get
            {
                return Path.GetExtension(FilePath).Equals(".gif", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public bool IsBig
        {
            get
            {
                return this.myFile.GetFileSize(FilePath) > 1024;
            }
        }

        public bool IsValid
        {
            get
            {
                return IsPicture || IsBig;
            }
        }

        public string DestinationPath
        {
            get
            {
                var subfolder = "";

                if (IsPicture)
                {
                    subfolder = @"smallPictures";

                    if (IsBig)
                    {
                        subfolder = @"bigPictures";
                    }
                } else if (IsBig)
                {
                    subfolder = @"bigFiles";
                }

                var result = !String.IsNullOrEmpty(subfolder) ? Path.Combine("isValid", subfolder) : "";

                return Path.Combine(result, FileName);
            }
        }
    }
}