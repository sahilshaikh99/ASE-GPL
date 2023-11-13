using ASEProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for verifying the functionality of the FileHandler.
    /// </summary>
    [TestClass]
    public class FileHandlerTest
    {
        /// <summary>
        /// Test case to verify that saving data to a file works as expected.
        /// </summary>
        [TestMethod]
        public void SaveDataToFile()
        {
            FileHandler fileHandler = new FileHandler();
            string filePath = "testFile.gpl";
            string data = "circle 50" +
                "moveto 100 100" +
                "circle 30" +
                "rectangle 100 100";

            fileHandler.SaveDataToFile(filePath, data);

            string savedData = System.IO.File.ReadAllText(filePath);
            Assert.AreEqual(data, savedData);
        }

        /// <summary>
        /// Test case to verify that reading content from a file works as expected.
        /// </summary>
        [TestMethod]
        public void ReadFileContent()
        {
            FileHandler fileHandler = new FileHandler();
            string filePath = "testFile.gpl";
            string expectedContent = "circle 50" +
                "moveto 100 100" +
                "circle 30" +
                "rectangle 100 100";

            // Create a test file with the expected content
            System.IO.File.WriteAllText(filePath, expectedContent);

            string actualContent = fileHandler.ReadFileContent(filePath);

            Assert.AreEqual(expectedContent, actualContent);
        }
    }
}
