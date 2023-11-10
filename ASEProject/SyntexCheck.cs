using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASEProject
{
    public class SyntexCheck
    {
        private Bitmap canvasBitmap;
        private PictureBox canvasShape;
        private List<string> exceptionMessages = new List<string>(); 

        public SyntexCheck(int canvasWidth, int canvasHeight, PictureBox canvasShape)
        {
            this.canvasShape = canvasShape;
            canvasBitmap = new Bitmap(canvasWidth, canvasHeight);
        }

        public void executeSyntexCheck(string command)
        {
            exceptionMessages.Clear();
            string[] commands = command.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int lineNumber = 0; lineNumber < commands.Length; lineNumber++)
            { 
                try
                {
                    var (shapeName, x, y, width, height, radius, penColorName, fill) = new CommandParser().ParseCommand(commands[lineNumber], canvasBitmap.Width, canvasBitmap.Height);

                    if (shapeName == null)
                    { 
                        throw new ArgumentException("Invalid command or coordinates are out of bounds.");
                    }
                }
                catch (ArgumentException ex)
                {
                    exceptionMessages.Add($"Error at line {lineNumber + 1} :" + ex.Message);

                }
            }
            ShowException();

        }
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

        public Image GetCanvasImage()
        {
            return (Image)canvasBitmap.Clone();
        }
    }
}
