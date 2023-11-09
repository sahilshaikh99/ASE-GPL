using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ASEProject;

namespace ASEProjectTest
{
    [TestClass]
    public class ClearCommandTest
    {

        [TestMethod]
        public void TestClearCommand()
        {
            CommandParser parser = new CommandParser();
            int canvasWidth = 800;
            int canvasHeight = 600;
            string clearCommand = "clear";

            var result = parser.ParseCommand(clearCommand, canvasWidth, canvasHeight);

            Assert.AreEqual("clear", result.ShapeName);
            Assert.AreEqual(0, result.X);
            Assert.AreEqual(0, result.Y);
            Assert.AreEqual(0, result.Width);
            Assert.AreEqual(0, result.Height);
            Assert.AreEqual(0, result.Radius);
            Assert.AreEqual(null, result.penColorName);
            Assert.AreEqual(true, result.Fill);

        }
    }

}

