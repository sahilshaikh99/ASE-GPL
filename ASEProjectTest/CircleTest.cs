using ASEProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace ASEProjectTest
{
    [TestClass]
    public class CircleTest
    {
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

            circle.Draw(graphics, penColor, x, y, 0, 0, radius, fill);

            Color expectedColor = Color.FromArgb(255, 0, 0, 0); // Assuming black is expected
         
            Color pixelColor = bitmap.GetPixel(x, y);

            Assert.AreEqual(expectedColor, pixelColor);
        }

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
