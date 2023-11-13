using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASEProject;
using System.Drawing;
using System.Windows.Forms;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for verifying the functionality of executing multiline commands with the DrawHandler.
    /// </summary>
    [TestClass]
    public class MultilineCommandTests
    {
        /// <summary>
        /// Test case to verify that executing correct multiline commands draws shapes on the canvas.
        /// </summary>
        [TestMethod]
        public void ExecuteMultilineCommand_CorrectCommands_DrawsShapes()
        {
            PictureBox pictureBox = new PictureBox();
            DrawHandler drawHandler = new DrawHandler(800, 600, pictureBox);
            string inputCommands = "pen red\nrectangle 10 10\ncircle 100\n";

            drawHandler.ExecuteMultilineCommand(inputCommands);
            Bitmap canvasImage = (Bitmap)drawHandler.GetCanvasImage();

            // Checking if the canvasImage contains a red rectangle at (10, 10)
            Assert.AreEqual(Color.Red, canvasImage.GetPixel(10, 10));
        }
    }
}
