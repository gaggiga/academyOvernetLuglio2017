using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.Test
{
    public class FileSystem : IFile
    {
        public string[] GetFiles(string path)
        {
            return System.IO.Directory.GetFiles(path);
        }

        public long GetFileSize(string filePath)
        {
            return new System.IO.FileInfo(filePath).Length;
        }

        public void Move(string fileSource, string fileDestination)
        {
            System.IO.File.Move(fileSource, fileDestination);
        }
    }
}
