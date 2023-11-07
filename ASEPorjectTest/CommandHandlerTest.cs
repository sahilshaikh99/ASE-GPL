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

    }
}