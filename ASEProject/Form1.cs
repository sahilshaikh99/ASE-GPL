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
        private Color penColor = Color.Black;
        private bool fillShapes = false;
        private List<Shape> myShapes = new List<Shape>();

        private CommandHandler CommandHandler = new CommandHandler();

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
            using (SaveFileDialog saveFileDialogBox = new SaveFileDialog())
            {
                saveFileDialogBox.Filter = "Custom Files (*.gpl)|*.gpl|All Files (*.*)|*.*";
                saveFileDialogBox.FilterIndex = 1;
                saveFileDialogBox.Title = "Save Commands File";
                saveFileDialogBox.DefaultExt = ".gpl";

                if (saveFileDialogBox.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = saveFileDialogBox.FileName;
                        string commandsToSave = programWindow.Text;

                        System.IO.File.WriteAllText(filePath, commandsToSave);
                        MessageBox.Show("Commands saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while saving commands: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialogBox = new OpenFileDialog())
            {
                openFileDialogBox.Filter = "Custom Files (*.gpl)|*.gpl|All Files (*.*)|*.*";
                openFileDialogBox.FilterIndex = 1;
                openFileDialogBox.Title = "Open Commands File";

                if (openFileDialogBox.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialogBox.FileName;
                        string fileContent = System.IO.File.ReadAllText(filePath);
                        programWindow.Text = fileContent;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while opening file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string userInput = commandBox.Text;
            string inputCommands = programWindow.Text;


            if (userInput != null && userInput.Length > 0)
            {
                using (Graphics graphics = Graphics.FromImage(canvasBitmap))
                {
                    executeCommand(userInput, graphics);
                    canvasShape.Image = (Image)canvasBitmap.Clone();

                }

            }
            if (inputCommands != null && inputCommands.Length > 0)
            {
                makeCanvasBlank();

                string[] commands = inputCommands.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                using (Graphics graphics = Graphics.FromImage(canvasBitmap))
                {
                    foreach (string command in commands)
                    {
                        executeCommand(command, graphics);
                        canvasShape.Image = (Image)canvasBitmap.Clone();
                    }
                }
            }

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {

            makeCanvasBlank();

            commandBox.Text = string.Empty;
            programWindow.Text = string.Empty;
        }



        private void executeCommand(string userInput, Graphics graphics)
        {
            try
                    {
                        var (shapeName, x, y, width, height, radius, penColorName, fill) = new CommandParser().ParseCommand(userInput, canvasShape.Width, canvasShape.Height);
                        if (shapeName != null)
                        {
                            if (shapeName == "moveto")
                            {
                                CommandHandler.MoveTo(x, y);
                            }
                            else if (shapeName == "pen")
                            {
                                penColor = Color.FromName(penColorName);
                            }
                            else if (shapeName == "fill")
                            {
                                fillShapes = fill;
                            }
                            else if (shapeName == "reset")
                            {
                                CommandHandler.ResetCursor();
                            }
                            else if (shapeName == "clear")
                            {
                                ClearMyCanvas();
                            }
                            else
                            {

                                Shape shape = new CreateShape().MakeShape(shapeName);

                                if (shape != null)
                                {
                                    myShapes.Add(shape);

                                    shape.Draw(graphics, penColor, CommandHandler.CursorPosX, CommandHandler.CursorPosY, width, height, radius, fillShapes);
                                }

                                else
                                {
                                    MessageBox.Show("Unknown shape.");
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

                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
        }

        private void ClearMyCanvas()
        {
            myShapes.Clear();
            makeCanvasBlank();
        }

        private void programWindow_TextChanged(object sender, EventArgs e)
        {

        }

        private void makeCanvasBlank()
        {
            canvasBitmap = new Bitmap(canvasShape.Width, canvasShape.Height);

            canvasShape.Image = canvasBitmap;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {

                    penColor = colorDialog.Color;
                }
            }
        }

        private void canvasShape_Click(object sender, EventArgs e)
        {

        }
    }

}
