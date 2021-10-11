using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_prg282.DataAccessLayer
{
    interface IFileControl
    {
        List<string> ReadTextFile();
        void WriteToTextFile(string text);
    }
}
