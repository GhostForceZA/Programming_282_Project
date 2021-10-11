using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace project_prg282.DataAccessLayer
{
    class FileHandler : IFileControl
    {
        string fileName = "Users.txt";

        public List<string> ReadTextFile()
        {
            StreamReader reader = new StreamReader(fileName);
            string line;
            List<string> users = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                users.Add(line);
            }

            return users;
        }

        public void WriteToTextFile(string text)
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(text);
            }
        }
    }
}
