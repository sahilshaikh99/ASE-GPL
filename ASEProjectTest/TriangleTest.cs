using ASEProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for verifying the functionality of the Triangle shape and its command parsing.
    /// </summary>
    [TestClass]
    public class TriangleTest
    {
        /// <summary>
        /// Test case to verify drawing of a triangle with valid parameters.
        /// </summary>
        [TestMethod]
        public void TestDrawTriangle_ValidParameters()
        {
            Triangle triangle = new Triangle();
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            Color penColor = Color.Black;
            int x = 100;
            int y = 100;
            int width = 80;
            int height = 60;
            int radius = 30;
            bool fill = true;

            triangle.Draw(graphics, penColor, x, y, width, height, radius, fill, 0);

            Color expectedColor = Color.FromArgb(255, 0, 0, 0);
            Color pixelColor = bitmap.GetPixel(x, y);
            Assert.AreEqual(expectedColor, pixelColor);
        }

        /// <summary>
        /// Test case to verify that attempting to draw a triangle with invalid dimensions throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void TestDrawTriangle_InvalidDimensions_ThrowsArgumentException()
        {
            CommandParser parser = new CommandParser();
            string inputCommand = "triangle -10 40 30";

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var (shapeName, x, y, width, height, radius, penColorName, fill) = parser.ParseCommand(inputCommand, 858, 477);
            });
        }

        /// <summary>
        /// Test case to verify that attempting to draw a triangle with missing parameters throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void TestDrawTriangle_MissingParameter_ThrowsArgumentException()
        {
            CommandParser parser = new CommandParser();
            string inputCommand = "triangle -10 40";

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var (shapeName, x, y, width, height, radius, penColorName, fill) = parser.ParseCommand(inputCommand, 858, 477);
            });
        }
    }
}
