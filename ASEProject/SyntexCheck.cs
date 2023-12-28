using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASEProject
{
    /// <summary>
    /// This class is responsible for syntax checking of commands and displaying any syntax errors on a canvas.
    /// </summary>
    public class SyntexCheck
    {
        private Bitmap canvasBitmap;
        private PictureBox canvasShape;
        private List<string> exceptionMessages = new List<string>();
        private readonly CommandParser commandParser;


        /// <summary>
        /// To initialize a new instance of the <see cref="SyntexCheck"/> class.
        /// </summary>
        /// <param name="canvasWidth">Width of the canvas.</param>
        /// <param name="canvasHeight">Height of the canvas.</param>
        /// <param name="canvasShape">The PictureBox used as the canvas.</param>
        public SyntexCheck(int canvasWidth, int canvasHeight, PictureBox canvasShape)
        {
            this.canvasShape = canvasShape;
            canvasBitmap = new Bitmap(canvasWidth, canvasHeight);
            commandParser = new CommandParser(this); 
        }

        /// <summary>
        /// Executes syntax checking on the provided command and displays any syntax errors on the canvas.
        /// </summary>
        /// <param name="command">The user input command to be checked for syntax errors.</param>
        public void ExecuteSyntaxCheck(string command)
        {
            exceptionMessages.Clear();
            string[] commands = command.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int lineNumber = 0; lineNumber < commands.Length; lineNumber++)
            {
                try
                {
                    var (shapeName, x, y, width, height, radius, penColorName, fill) = commandParser.ParseCommand(commands[lineNumber], canvasBitmap.Width, canvasBitmap.Height);

                    if (shapeName == null)
                    {
                        throw new ArgumentException("Invalid command or coordinates are out of bounds.");
                    }
                }
                catch (ArgumentException ex)
                {
                    //Add exception to exception list
                    exceptionMessages.Add($"Error at line {lineNumber + 1} : {ex.Message}");
                }
            }
            ShowException();
        }

        /// <summary>
        /// Displays the syntax error messages on the canvas.
        /// </summary>
        public void ShowException()
        {
            canvasBitmap = new Bitmap(canvasBitmap.Width, canvasBitmap.Height);
            canvasShape.Image = (Image)canvasBitmap.Clone();

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
        /// Gets the image of the canvas with displayed syntax error messages.
        /// </summary>
        /// <returns>The image of the canvas with syntax error messages.</returns>
        public Image GetCanvasImage()
        {
            return (Image)canvasBitmap.Clone();
        }
    }
}
