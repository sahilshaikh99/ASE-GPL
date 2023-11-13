using ASEProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for verifying the functionality of the DrawHandler.
    /// </summary>
    [TestClass]
    public class DrawHandlerTest
    {
        /// <summary>
        /// Test case to verify that executing the PenColor command updates the pen color correctly.
        /// </summary>
        [TestMethod]
        public void ExecutePenColorCommand_ValidColor_PenColorUpdated()
        {
            PictureBox pictureBox = new PictureBox();

            DrawHandler drawHandler = new DrawHandler(800, 600, null);
            string penColorCommand = "pen blue";

            drawHandler.ExecuteCommand(penColorCommand);
            Color penColor = drawHandler.GetPenColor();

            Assert.AreEqual(Color.Blue, penColor);
        }

        /// <summary>
        /// Test case to verify that setting an invalid pen color throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void SetPenColor_InvalidColor_CommandHandlerException()
        {
            var drawHandler = new DrawHandler(800, 600);
            string invalidColorName = "invalidcolor";

            Assert.ThrowsException<ArgumentException>(() => drawHandler.SetPenColor(invalidColorName));
        }

        /// <summary>
        /// Test case to verify that executing a Fill command with an invalid fill parameter throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_FillCommand_WithInvalidFillParameter_CommandHandlerException()
        {
            var drawHandler = new DrawHandler(800, 600);
            string invalidFillCommand = "fill unknown";

            Assert.ThrowsException<ArgumentException>(() => drawHandler.ExecuteCommand(invalidFillCommand));
        }
    }
}
