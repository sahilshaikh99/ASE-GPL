
using ASEProject;

namespace ASEProjectTest
{
    [TestClass]
    public class CommandHandlerTest
    {
        [TestMethod]
        public void MoveToCommand_UpdatesCursorPosition()
        {
            
            CommandHandler commandHandler = new CommandHandler();
            int expectedX = 100;
            int expectedY = 200;

            commandHandler.MoveTo(expectedX, expectedY);
            int actualX = commandHandler.CursorPosX;
            int actualY = commandHandler.CursorPosY;

            Assert.AreEqual(expectedX, actualX);
            Assert.AreEqual(expectedY, actualY);
        }

        [TestMethod]
        public void ResetCursor_ResetsCursorPositions()
        {
            
            CommandHandler commandHandler = new CommandHandler();
            commandHandler.CursorPosX = 42; 
            commandHandler.CursorPosX = 24;

            commandHandler.ResetCursor(); 

            Assert.AreEqual(0, commandHandler.CursorPosX); // Ensure cursor positions are reset to 0
            Assert.AreEqual(0, commandHandler.CursorPosY);
        }
    }
}