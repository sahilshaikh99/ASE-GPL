using ASEProject;
using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for verifying the functionality of the Rectangle shape and its command parsing.
    /// </summary>
    [TestClass]
    public class RectangleTest
    {
        /// <summary>
        /// Test case to verify the parsing of a valid rectangle command.
        /// </summary>
        [TestMethod]
        public void TestRectangleCommandParsing()
        {
            CommandParser parser = new CommandParser();
            string inputCommand = "rectangle 100 50";

            var (shapeName, x, y, width, height, radius, penColorName, fill) = parser.ParseCommand(inputCommand, 858, 477);

            Assert.AreEqual("rectangle", shapeName);
            Assert.AreEqual(0, x);
            Assert.AreEqual(0, y);
            Assert.AreEqual(100, width);
            Assert.AreEqual(50, height);
            Assert.AreEqual(0, radius);
            Assert.IsNull(penColorName);
            Assert.IsTrue(fill);
        }

        /// <summary>
        /// Test case to verify parsing of a rectangle command with invalid input.
        /// </summary>
        [TestMethod]
        public void TestRectangleCommandParsingWithInvalidInput()
        {
            CommandParser parser = new CommandParser();
            string inputCommand = "rectangle invalid invalid";

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var (shapeName, x, y, width, height, radius, penColorName, fill) = parser.ParseCommand(inputCommand, 858, 477);
            });
        }

        /// <summary>
        /// Test case to verify the drawing of a rectangle shape.
        /// </summary>
        [TestMethod]
        public void TestParseCommand_DrawRectangle()
        {
            CommandParser commandParser = new CommandParser();
            int canvasWidth = 200;
            int canvasHeight = 200;
            string command = "rectangle 50 40";

            Bitmap bitmap = new Bitmap(canvasWidth, canvasHeight);
            Graphics graphics = Graphics.FromImage(bitmap);

            var result = commandParser.ParseCommand(command, canvasWidth, canvasHeight);

            Assert.AreEqual("rectangle", result.ShapeName);
            Assert.AreEqual(50, result.Width);
            Assert.AreEqual(40, result.Height);
            Assert.AreEqual(0, result.Radius);
            Assert.AreEqual(true, result.Fill);

            ASEProject.Rectangle myrectangle = new ASEProject.Rectangle();
            myrectangle.Draw(graphics, Color.Black, 100, 100, result.Width, result.Height, result.Radius, result.Fill, 0);

            // Check if the drawn pixel has the expected color
            Color expectedColor = Color.FromArgb(255, 0, 0, 0); // Assuming black is expected
            Color pixelColor = bitmap.GetPixel(100, 100);
            Assert.AreEqual(expectedColor, pixelColor);
        }
    }
}
