using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ASEProject
{
    public partial class Form1 : Form
    {
        private Bitmap canvasBitmap;

        public Form1()
        {
            InitializeComponent();
            canvasBitmap = new Bitmap(canvasShape.Width, canvasShape.Height);
        }
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string userInput = commandBox.Text;


            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                var (shapeName, x, y) = new CommandParser().ParseCommand(userInput, canvasShape.Width, canvasShape.Height);
                if (shapeName != null)
                {
                    Shape shape = new CreateShape().MakeShape(shapeName);

                    if (shape != null)
                    {
                        shape.Draw(graphics, x, y);
                    }

                    else
                    {
                        MessageBox.Show("Unknown shape.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid command or coordinates are out of bounds.");
                }

            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            canvasShape.Image = null;

            commandBox.Text = string.Empty;
        }

        private void programWindow_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
