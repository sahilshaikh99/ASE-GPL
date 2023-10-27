using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class CommandParser
    {
        public (string ShapeName, int X, int Y, int Width, int Height, int Radius) ParseCommand(string command, int canvasWidth, int canvasHeight)
        {
            string[] parts = command.Split(' ');
            string shapeName = parts[0].ToLower();

            int width = 0;
            int height = 0;
            int radius = 0;

            if (shapeName == "rectangle" && parts.Length == 3)
            {
                if (int.TryParse(parts[1], out width) && int.TryParse(parts[2], out height))
                {

                }
            }
            else if (shapeName == "circle" && parts.Length == 2)
            {
                if (int.TryParse(parts[1], out radius))
                {
                }
            }
            else if (shapeName == "triangle")
            {
            }


            return (shapeName, 0, 0, width, height, radius);
        }
    }

}
