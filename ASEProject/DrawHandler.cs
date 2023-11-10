using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ASEProject
{
    /// <summary>
    /// This class manages drawing shapes on a canvas, handling commands and maintaining state.
    /// </summary>
    public class DrawHandler
    {
        private Bitmap canvasBitmap;
        private Color penColor = Color.Black;
        private bool fillShapes = false;
        private List<Shape> myShapes = new List<Shape>();
        private CommandHandler commandHandler = new CommandHandler();
        private PictureBox canvasShape;
        private List<string> exceptionMessages = new List<string>();

        /// <summary>
        /// Initializes a new instance of the DrawHandler class.
        /// </summary>
        /// <param name="canvasWidth">The width of the canvas.</param>
        /// <param name="canvasHeight">The height of the canvas.</param>
        /// <param name="canvasShape">The PictureBox representing the canvas (optional).</param>
        public DrawHandler(int canvasWidth, int canvasHeight, PictureBox canvasShape = null)
        {
            this.canvasShape = canvasShape;
            canvasBitmap = new Bitmap(canvasWidth, canvasHeight);
        }

        /// <summary>
        /// Executes a single drawing command.
        /// </summary>
        /// <param name="command">The drawing command to execute.</param>
        /// <param name="LineNumber">The line number of the command in a multiline input (optional).</param>
        public void ExecuteCommand(string command, int LineNumber = 0)
        {

            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                try
                {
                    var (shapeName, x, y, width, height, radius, penColorName, fill) = new CommandParser().ParseCommand(command, canvasBitmap.Width, canvasBitmap.Height);

                    if (shapeName != null)
                    {
                        if (shapeName == "moveto")
                        {
                            commandHandler.MoveTo(x, y);
                        }
                        else if (shapeName == "pen")
                        {
                            SetPenColor(Color.FromName(penColorName));
                        }
                        else if (shapeName == "fill")
                        {
                            fillShapes = fill;
                        }
                        else if (shapeName == "reset")
                        {
                            commandHandler.ResetCursor();
                        }
                        else if (shapeName == "clear")
                        {
                            ClearCanvas();
                        }
                        else
                        {
                            Shape shape = new CreateShape().MakeShape(shapeName);

                            if (shape != null)
                            {
                                myShapes.Add(shape);
                                shape.Draw(graphics, penColor, commandHandler.CursorPosX, commandHandler.CursorPosY, width, height, radius, fillShapes);
                            }
                            else
                            {
                                throw new ArgumentException("Unknown shape.");
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid command or coordinates are out of bounds.");
                    }
                }
                catch (ArgumentException ex)
                {
                    exceptionMessages.Add($"Error at line {LineNumber + 1} :" + ex.Message);
                }
            }
            ShowException();
        }

        /// <summary>
        /// Executes a single drawing command.
        /// </summary>
        /// <param name="inputCommand">The drawing command to execute.</param>
        public void ExecuteSingleCommand(string inputCommand)
        {
            //CleanUpCanvas();
            ExecuteCommand(inputCommand);
            ShowException();
        }

        /// <summary>
        /// Executes a multiline drawing command.
        /// </summary>
        /// <param name="inputCommands">The multiline drawing commands to execute.</param>
        public void ExecuteMultilineCommand(string inputCommands)
        {
            penColor = Color.Black;
            fillShapes = false;
            CleanUpCanvas();
            string[] commands = inputCommands.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                for (int lineNumber = 0; lineNumber < commands.Length; lineNumber++)
                {
                    ExecuteCommand(commands[lineNumber], lineNumber);
                }
            }
            ShowException();
        }

        /// <summary>
        /// Gets the current canvas image.
        /// </summary>
        /// <returns>The current canvas image.</returns>
        public Image GetCanvasImage()
        {
            return (Image)canvasBitmap.Clone();
        }

        /// <summary>
        /// Clears the canvas and exception messages.
        /// </summary>
        public void ClearCanvas()
        {
            exceptionMessages.Clear();
            myShapes.Clear();
            CleanUpCanvas();
        }

        /// <summary>
        /// Clears the canvas bitmap and updates the canvas image.
        /// </summary>
        public void CleanUpCanvas()
        {
            exceptionMessages.Clear();
            canvasBitmap = new Bitmap(canvasBitmap.Width, canvasBitmap.Height);
            UpdateCanvasImage();
        }

        /// <summary>
        /// Displays exception messages on the canvas.
        /// </summary>
        public void ShowException()
        {
            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                using (Font font = new Font("Calibri", 10))
                using (SolidBrush brush = new SolidBrush(Color.Red))
                {
                    int y = 10;
                    foreach (string message in exceptionMessages)
                    {
                        graphics.DrawString(message, font, brush, 10, y);
                        y += 20;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the canvas image on the PictureBox.
        /// </summary>
        private void UpdateCanvasImage()
        {
            canvasShape.Image = (Image)canvasBitmap.Clone();
        }

        /// <summary>
        /// Sets the pen color for drawing shapes.
        /// </summary>
        /// <param name="color">The color to set.</param>
        public void SetPenColor(Color color)
        {
            penColor = color;
        }

        /// <summary>
        /// Gets the current pen color.
        /// </summary>
        /// <returns>The current pen color.</returns>
        public Color GetPenColor()
        {
            return penColor;
        }

        /// <summary>
        /// Sets the pen color for drawing shapes.
        /// </summary>
        /// <param name="penColorName">The name of the color to set.</param>
        internal void SetPenColor(string penColorName)
        {
            throw new ArgumentException();
        }
    }
}
