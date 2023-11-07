using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class CommandHandler
    {
        public int CursorPosX { get; internal set; }
        public int CursorPosY { get; internal set; }

        public void MoveTo(int x, int y)
        {
            CursorPosX = x;
            CursorPosY = y;
        }

        public void ResetCursor()
        {
            // Reset the cursor position logic here
            CursorPosX = 0;
            CursorPosY = 0;
        }
    }
}
