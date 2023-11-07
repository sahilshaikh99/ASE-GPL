using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class FileHandler
    {
            public void SaveDataToFile(string filePath, string data)
            {
                // Logic to save data to a file
                System.IO.File.WriteAllText(filePath, data);
            }

            public string ReadFileContent(string filePath)
            {
                // Logic to read data from a file
                return System.IO.File.ReadAllText(filePath);
            }
    }
}
