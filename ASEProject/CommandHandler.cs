using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    /// <summary>
    /// This class handles method to move cursor and resetting cursor pointer.
    /// </summary>
    public class CommandHandler
    {
        /// <summary>
        /// Gets or sets the x-coordinate of the cursor position.
        /// </summary>
        public int CursorPosX { get; internal set; }

        /// <summary>
        /// Gets or sets the y-coordinate of the cursor position.
        /// </summary>
        public int CursorPosY { get; internal set; }

        /// <summary>
        /// Moves the cursor to the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate to move the cursor to.</param>
        /// <param name="y">The y-coordinate to move the cursor to.</param>
        public void MoveTo(int x, int y)
        {
            // Set the cursor position to the specified coordinates
            CursorPosX = x;
            CursorPosY = y;
        }

        /// <summary>
        /// Resets the cursor position to the default coordinates (0, 0).
        /// </summary>
        public void ResetCursor()
        {
            // Reset the cursor position to the default coordinates
            CursorPosX = 0;
            CursorPosY = 0;
        }
    }
}
