using ASEProject;

namespace ASEProjectTest
{
    [TestClass]
    public class FileHandlerTest
    {

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