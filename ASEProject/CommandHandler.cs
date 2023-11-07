using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class CommandHandler
    {
        public int CursorPosX { get; private set; }
        public int CursorPosY { get; private set; }

        public void MoveTo(int x, int y)
        {
            CursorPosX = x;
            CursorPosY = y;
        }
    }
}
