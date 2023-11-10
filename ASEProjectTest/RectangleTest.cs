using ASEProject;
using System.Drawing;
using System.Windows;

namespace ASEProjectTest
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void TestRectangleCommandParsing()
        {
            CommandParser parser = new CommandParser();
            string inputCommand = "rectangle 100 50";

            var (shapeName, x, y, width, height, radius, penColorName, fill) = parser.ParseCommand(inputCommand, 858, 477);

            Assert.AreEqual("rectangle", shapeName);
            Assert.AreEqual(0, x);
            Assert.AreEqual(0, y);
            Assert.AreEqual(100, width); // Width is set based on the command
            Assert.AreEqual(50, height); // Height is set based on the command
            Assert.AreEqual(0, radius);
            Assert.IsNull(penColorName);
            Assert.IsTrue(fill);
        }

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

        [TestMethod]
        public void TestParseCommand_DrawRectangle()
        {
            CommandParser commandParser = new CommandParser();
            int canvasWidth = 200; // Set your canvas width
            int canvasHeight = 200; // Set your canvas height
            string command = "rectangle 50 40"; // A valid rectangle command

            // Create a Bitmap to serve as the drawing surface
            Bitmap bitmap = new Bitmap(canvasWidth, canvasHeight);
            Graphics graphics = Graphics.FromImage(bitmap);

            // Act
            var result = commandParser.ParseCommand(command, canvasWidth, canvasHeight);

            // Assert
            Assert.AreEqual("rectangle", result.ShapeName);
            Assert.AreEqual(50, result.Width);
            Assert.AreEqual(40, result.Height);
            Assert.AreEqual(0, result.Radius);
            Assert.AreEqual(true, result.Fill);

            // Create a Rectangle instance and draw it
            ASEProject.Rectangle myrectangle = new ASEProject.Rectangle();
            myrectangle.Draw(graphics, Color.Black, 100, 100, result.Width, result.Height, result.Radius, result.Fill);

            // For example, you can check specific pixel colors within the rectangle area.

            Color pixelColor = bitmap.GetPixel(100, 100); // Check a pixel within the drawn rectangle
            Assert.AreEqual(Color.Black, pixelColor); // Verify that the pixel color matches the pen color
        }
    }
}
