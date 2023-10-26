using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class CommandParser
    {
        public (string ShapeName, int X, int Y) ParseCommand(string command, int canvasWidth, int canvasHeight)
        {
            string[] parts = command.Split(' ');
            if (parts.Length < 3)
            {
                return (null, 0, 0);
            }

            string shapeName = parts[0].ToLower();
            if (!int.TryParse(parts[1], out int x) || !int.TryParse(parts[2], out int y))
            {
                return (null, 0, 0);
            }

            return (shapeName, x, y);
        }
    }

}
