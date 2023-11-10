using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class CommandParser
    {
        public (string ShapeName, int X, int Y, int Width, int Height, int Radius, String penColorName, bool Fill) ParseCommand(string command, int canvasWidth, int canvasHeight)
        {

            string[] parts = command.Split(' ');
            string shapeName = parts[0].ToLower();


            if (parts.Length < 1)
            {
                throw new ArgumentException(new ExceptionHandler().generateException(401, "", ""));
            }

            // Check for valid commands
            if (shapeName != "moveto" && shapeName != "pen" && shapeName != "fill" && shapeName != "rectangle" && shapeName != "circle" && shapeName != "triangle" && shapeName != "drawto")
            {
                throw new ArgumentException("Invalid command: " + shapeName);
            }


            if (shapeName == "moveto")
            {
                if (parts.Length < 3)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "moveto", "X and Y coordinates"));
                }

                if (int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                {
                    if (x >= 0 && x < canvasWidth && y >= 0 && y < canvasHeight)
                    {
                        return (shapeName, x, y, 0, 0, 0, null, true);
                    }
                }
                throw new ArgumentException(new ExceptionHandler().generateException(403, "moveto",""));

                //return (null, 0, 0, 0, 0, 0, null, true);
            }
            else if (shapeName == "pen")
            {
                if (parts.Length < 2)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "pen", "a color parameter"));
                }

                string penColorName = parts.Length > 1 ? parts[1] : null;

                Color penColor = Color.FromName(penColorName);
                if (penColor.ToArgb() == 0)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "pen", "valid color name"));
                }
                return (shapeName, 0, 0, 0, 0, 0, penColorName, true);
            }
            else if (shapeName == "fill")
            {
   
                if (parts.Length > 1)
                {
                    string fillOption = parts[1].ToLower();
                    if (fillOption == "on")
                    {
                        return (shapeName, 0, 0, 0, 0, 0, null, true);
                    }
                    else if (fillOption == "off")
                    {
                        return (shapeName, 0, 0, 0, 0, 0, null, false);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid 'fill' command: Use 'on' or 'off'.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid 'fill' command: Requires 'on' or 'off'.");

                }
            }

            int width = 0;
            int height = 0;
            int radius = 0;

            if (shapeName == "rectangle")
            {
                if (parts.Length < 3)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "rectangle", "width and height"));
                }

                if (int.TryParse(parts[1], out int rectWidth) && int.TryParse(parts[2], out int rectHeight))
                {
                    // Validate width and height
                    if (rectWidth <= 0 || rectHeight <= 0)
                    {
                        throw new ArgumentException(new ExceptionHandler().generateException(402, "rectangle", "positive width and height values"));
                    }

                    return (shapeName, 0, 0, rectWidth, rectHeight, 0, null, true);
                }
                else
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "rectangle", "numeric width and height values"));
                }
            }

            else if (shapeName == "circle")
            {
                if (parts.Length < 2)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "circle", "radius"));
                }

                if (int.TryParse(parts[1], out int circleRadius))
                {
                    // Validate radius
                    if (circleRadius <= 0)
                    {
                        throw new ArgumentException(new ExceptionHandler().generateException(402, "circle", "positive radius value"));
                    }

                    return (shapeName, 0, 0, 0, 0, circleRadius, null, true);
                }
                else
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "circle", "numeric radius value"));
                }
            }
            else if (shapeName == "triangle")
            {
                if (parts.Length < 4)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "triangle", "width, height, and radius"));
                }

                if (int.TryParse(parts[1], out int triangleWidth) && int.TryParse(parts[2], out int triangleHeight) && int.TryParse(parts[3], out int triangleRadius))
                {
                    // Validate width, height, and radius
                    if (triangleWidth <= 0 || triangleHeight <= 0 || triangleRadius <= 0)
                    {
                        throw new ArgumentException(new ExceptionHandler().generateException(402, "triangle", "positive width, height, and radius values"));
                    }

                    return (shapeName, 0, 0, triangleWidth, triangleHeight, triangleRadius, null, true);
                }
                else
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "triangle", "numeric width, height, and radius values"));
                }
            }
            else if (shapeName == "drawto")
            {
                if (int.TryParse(parts[1], out width) && int.TryParse(parts[2], out height))
                {
                }
            }


            return (shapeName, 0, 0, width, height, radius, null, true);
        }
    }

}
