using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.Test
{
    public interface IFile
    {
        void Move(string fileSource, string fileDestination);
        string[] GetFiles(string path);
    }
}
