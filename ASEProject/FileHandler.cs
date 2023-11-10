using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    /// <summary>
    /// This class provides methods for saving and reading data to/from a file.
    /// </summary>
    public class FileHandler
    {
        /// <summary>
        /// Saves the provided data to the specified file.
        /// </summary>
        /// <param name="filePath">The path to the file where data should be saved.</param>
        /// <param name="data">The data to be saved to the file.</param>
        public void SaveDataToFile(string filePath, string data)
        {
            // Logic to save data to a file
            System.IO.File.WriteAllText(filePath, data);
        }

        /// <summary>
        /// Reads the content of the specified file.
        /// </summary>
        /// <param name="filePath">The path to the file from which data should be read.</param>
        /// <returns>The content of the specified file as a string.</returns>
        public string ReadFileContent(string filePath)
        {
            // Logic to read data from a file
            return System.IO.File.ReadAllText(filePath);
        }
    }
}
