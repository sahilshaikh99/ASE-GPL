using ASEProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace ASEProjectTest
{
    [TestClass]
    public class CircleTest
    {
        /// <summary>
        /// Test case to verify that drawing a circle with valid parameters produces the expected color at the specified position.
        /// </summary>
        [TestMethod]
        public void TestDrawCircle_ValidParameters()
        {
            Circle circle = new Circle();
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            Color penColor = Color.Black;
            int x = 100;
            int y = 100;
            int radius = 50;
            bool fill = true;

            circle.Draw(graphics, penColor, x, y, 0, 0, radius, fill, 0);

            Color expectedColor = Color.FromArgb(255, 0, 0, 0); // Assuming black is expected
            Color pixelColor = bitmap.GetPixel(x, y);

            Assert.AreEqual(expectedColor, pixelColor);
        }

        /// <summary>
        /// Test case to verify that attempting to draw a circle with an invalid radius throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void TestDrawCircle_InvalidRadius_ThrowsArgumentException()
        {
            
            CommandParser parser = new CommandParser();
            string inputCommand = "circle -10";

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var (shapeName, x, y, width, height, radius, penColorName, fill) = parser.ParseCommand(inputCommand, 858, 477);
            });
        }
    }
}
