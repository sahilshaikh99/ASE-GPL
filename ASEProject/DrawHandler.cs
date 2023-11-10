using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ASEProject
{
    public class DrawHandler
    {
        private Bitmap canvasBitmap;
        private Color penColor = Color.Black;
        private bool fillShapes = false;
        private List<Shape> myShapes = new List<Shape>();
        private CommandHandler commandHandler = new CommandHandler();
        private PictureBox canvasShape;
        private string exceptionMessage = string.Empty;

        public DrawHandler(int canvasWidth, int canvasHeight, PictureBox canvasShape)
        {
            this.canvasShape = canvasShape;
            canvasBitmap = new Bitmap(canvasWidth, canvasHeight);
        }

        public void ExecuteCommand(string command)
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
                    exceptionMessage = "Error: " + ex.Message;
                   ShowException(exceptionMessage);
                }
            }
        }

        public void ExecuteMultilineCommand(string inputCommands)
        {
            ClearCanvas();

            string[] commands = inputCommands.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                foreach (string command in commands)
                {
                    ExecuteCommand(command);
                }
            }
        }

        public Image GetCanvasImage()
        {
            return (Image)canvasBitmap.Clone();
        }

        public void ClearCanvas()
        {
            myShapes.Clear();
            canvasBitmap = new Bitmap(canvasBitmap.Width, canvasBitmap.Height);
            UpdateCanvasImage();
        }

        public void ShowException(string occuredException)
        {
            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {

                // Draw the exception message
                using (Font font = new Font("Arial", 12))
                using (SolidBrush brush = new SolidBrush(Color.Red))
                {
                    graphics.DrawString(occuredException, font, brush, 10, 10);
                }
            }
        }

        private void UpdateCanvasImage()
        {

            canvasShape.Image = (Image)canvasBitmap.Clone();
        }

        public void SetPenColor(Color color)
        {
            penColor = color;
        }
        public Color GetPenColor()
        {
            return penColor;
        }

        internal void SetPenColor(string penColorName)
        {
            throw new ArgumentException();
        }
    }
}
