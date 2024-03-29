﻿using ASEProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for verifying the functionality of the DrawTo shape.
    /// </summary>
    [TestClass]
    public class DrawToTest
    {
        /// <summary>
        /// Test case to verify that drawing a DrawTo shape with valid parameters updates the pixel color as expected.
        /// </summary>
        [TestMethod]
        public void TestDrawTo_ValidParameters()
        {
            DrawTo drawto = new DrawTo();
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            Color penColor = Color.Black;
            int x = 100;
            int y = 100;
            int width = 80;
            int height = 60;
            int radius = 30;
            bool fill = true;

            drawto.Draw(graphics, penColor, x, y, width, height, radius, fill, 0);

            Color expectedColor = Color.FromArgb(255, 0, 0, 0);
            Color pixelColor = bitmap.GetPixel(x, y);
            Assert.AreEqual(expectedColor, pixelColor);
        }

        /// <summary>
        /// Test case to verify that drawing a DrawTo shape with invalid dimensions throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void TestDrawTo_InvalidDimensions_ThrowsArgumentException()
        {
            CommandParser parser = new CommandParser();
            string inputCommand = "drawto -100 100";

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var (shapeName, x, y, width, height, radius, penColorName, fill) = parser.ParseCommand(inputCommand, 858, 477);
            });
        }

        /// <summary>
        /// Test case to verify that drawing a DrawTo shape with missing parameters throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void TestDrawto_MissingParameter_ThrowsArgumentException()
        {
            CommandParser parser = new CommandParser();
            string inputCommand = "drawto -10";

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var (shapeName, x, y, width, height, radius, penColorName, fill) = parser.ParseCommand(inputCommand, 858, 477);
            });
        }
    }
}
