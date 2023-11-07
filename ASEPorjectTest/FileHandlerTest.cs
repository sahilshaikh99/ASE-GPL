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


    }
}