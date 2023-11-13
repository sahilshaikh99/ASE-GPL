using ASEProject;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for verifying the functionality of the CommandHandler.
    /// </summary>
    [TestClass]
    public class CommandHandlerTest
    {
        /// <summary>
        /// Test case to verify that the MoveTo command updates the cursor position correctly.
        /// </summary>
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

        /// <summary>
        /// Test case to verify that attempting to move to a position outside the canvas bounds throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void MoveToCommand_OutOfBounds_ThrowsArgumentException()
        {
            CommandParser parser = new CommandParser();
            int canvasWidth = 800;
            int canvasHeight = 600;

            // Attempt to move to a position outside the canvas
            Assert.ThrowsException<ArgumentException>(() => parser.ParseCommand("moveto 1000 1000", canvasWidth + 1, canvasHeight + 1));
        }

        /// <summary>
        /// Test case to verify that the ResetCursor command resets the cursor positions to (0, 0).
        /// </summary>
        [TestMethod]
        public void ResetCursor_ResetsCursorPositions()
        {
            CommandHandler commandHandler = new CommandHandler();
            commandHandler.CursorPosX = 42;
            commandHandler.CursorPosX = 24;

            commandHandler.ResetCursor();

            Assert.AreEqual(0, commandHandler.CursorPosX);
            Assert.AreEqual(0, commandHandler.CursorPosY);
        }
    }
}
