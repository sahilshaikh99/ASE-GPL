using ASEProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProjectTest
{
    [TestClass]
    public class DrawHandlerTest
    {
        [TestMethod]
        public void ExecutePenColorCommand_ValidColor_PenColorUpdated()
        {
            DrawHandler drawHandler = new DrawHandler(800, 600);
            string penColorCommand = "pen blue";

            drawHandler.ExecuteCommand(penColorCommand);
            Color penColor = drawHandler.GetPenColor();

            Assert.AreEqual(Color.Blue, penColor);
         }

        [TestMethod]
        public void SetPenColor_InvalidColor_CommandHandlerException()
        {
            var drawHandler = new DrawHandler(800, 600);
            string invalidColorName = "invalidcolor";

            Assert.ThrowsException<ArgumentException>(() => drawHandler.SetPenColor(invalidColorName));
        }

    }
}
