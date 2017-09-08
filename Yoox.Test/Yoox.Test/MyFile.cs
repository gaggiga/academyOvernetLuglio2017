using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.Test
{
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

        public string DestinationFolder
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
                }
                else if (IsBig)
                {
                    subfolder = @"bigFiles";
                }

                var result = !String.IsNullOrEmpty(subfolder) ? Path.Combine("isValid", subfolder) : "notValid";

                return result;
            }
        }

        public string DestinationPath
        {
            get
            {
                return Path.Combine(DestinationFolder, FileName);
            }
        }
    }
}
