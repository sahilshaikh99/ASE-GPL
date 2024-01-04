using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    /// <summary>
    /// This class is responsible for parsing drawing commands and extracting relevant information.
    /// </summary>
    public class CommandParser
    {
        private readonly VariableManager variableManager = VariableManager.Instance;
        private readonly DrawHandler drawHandler;
        private readonly SyntexCheck syntexCheck;

        public CommandParser(DrawHandler drawHandler)
        {
            this.variableManager = VariableManager.Instance;
            this.drawHandler = drawHandler;
   
        }

        public CommandParser(SyntexCheck syntexCheck)
        {
            this.syntexCheck = syntexCheck;
        }

        public CommandParser()
        {
        }

        /// <summary>
        /// Parses a drawing command and extracts relevant information.
        /// </summary>
        /// <param name="command">The drawing command to parse.</param>
        /// <param name="canvasWidth">The width of the canvas.</param>
        /// <param name="canvasHeight">The height of the canvas.</param>
        /// <returns>A data containing information about the drawing command.</returns>
        public (string ShapeName, int X, int Y, int Width, int Height, int Radius, String penColorName, bool Fill) ParseCommand(string command, int canvasWidth, int canvasHeight)
        {
            string[] parts = command.Split(' ');
            string shapeName = parts[0].ToLower().Trim();

            if (parts.Length < 1)
            {
                throw new ArgumentException(new ExceptionHandler().generateException(401, "", ""));
            }

            // Check for valid commands
            if (shapeName != "moveto" && shapeName != "colour" && shapeName != "fill" && shapeName != "rectangle" && shapeName != "circle" && shapeName != "triangle" && shapeName != "drawto" && shapeName != "clear" && shapeName != "reset" && shapeName != "rotate" && shapeName != "endwhile")
            {
                throw new ArgumentException("Invalid command: " + shapeName);
            }

            else if (shapeName == "moveto")
            {
                if (parts.Length < 3)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "moveto", "X and Y coordinates"));
                }

                int x, y;

                if ((int.TryParse(parts[1], out x) || TryGetVariableValue(parts[1], out x)) &&
                    (int.TryParse(parts[2], out y) || TryGetVariableValue(parts[2], out y)))
                {
                    if (x >= 0 && x < canvasWidth && y >= 0 && y < canvasHeight)
                    {
                        return (shapeName, x, y, 0, 0, 0, null, true);
                    }
                }

                throw new ArgumentException(new ExceptionHandler().generateException(403, "moveto", ""));
            }

            else if (shapeName == "colour")
            {
                if (parts.Length < 2)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "colour", "a color parameter"));
                }

                string penColorName = parts.Length > 1 ? parts[1] : null;

                Color penColor = Color.FromName(penColorName);
                if (penColor.ToArgb() == 0)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "colour", "valid color name"));
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

                int rectWidth, rectHeight;

                if ((int.TryParse(parts[1], out rectWidth) || TryGetVariableValue(parts[1], out rectWidth)) &&
                    (int.TryParse(parts[2], out rectHeight) || TryGetVariableValue(parts[2], out rectHeight)))
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

                int circleRadius;

                if (int.TryParse(parts[1], out circleRadius) || TryGetVariableValue(parts[1], out circleRadius))
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

                int triangleWidth, triangleHeight, triangleRadius;

                if ((int.TryParse(parts[1], out triangleWidth) || TryGetVariableValue(parts[1], out triangleWidth)) &&
                    (int.TryParse(parts[2], out triangleHeight) || TryGetVariableValue(parts[2], out triangleHeight)) &&
                    (int.TryParse(parts[3], out triangleRadius) || TryGetVariableValue(parts[3], out triangleRadius)))
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
                if (parts.Length < 3)
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "drawto", "width and height"));
                }

                int drawtoWidth, drawtoHeight;

                if ((int.TryParse(parts[1], out drawtoWidth) || TryGetVariableValue(parts[1], out drawtoWidth)) &&
                    (int.TryParse(parts[2], out drawtoHeight) || TryGetVariableValue(parts[2], out drawtoHeight)))
                {
                    // Validate width and height
                    if (drawtoWidth <= 0 || drawtoHeight <= 0)
                    {
                        throw new ArgumentException(new ExceptionHandler().generateException(402, "drawto", "positive width and height values"));
                    }

                    return (shapeName, 0, 0, drawtoWidth, drawtoHeight, 0, null, true);
                }
                else
                {
                    throw new ArgumentException(new ExceptionHandler().generateException(402, "drawto", "numeric width and height values"));
                }
            }


            return (shapeName, 0, 0, width, height, radius, null, true);
        }

        // New method to parse rotate command
        public float ParseRotateCommand(string command)
        {
            string[] parts = command.Split(' ');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid rotate command. Usage: rotate <angle>");
            }

            float angle;
            if (!float.TryParse(parts[1], out angle))
            {
                throw new ArgumentException("Invalid angle specified in rotate command.");
            }

            return angle;
        }

        // Helper method to try to get the variable value
        private bool TryGetVariableValue(string variableName, out int variableValue)
        {
            variableValue = variableManager.GetVariableValue(variableName);
            return variableValue > 0;
        }
    }
}
