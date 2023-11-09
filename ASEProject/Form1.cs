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
        private DrawHandler drawHandler;
        private FileHandler FileHandler = new FileHandler();

        public Form1()
        {
            InitializeComponent();
            drawHandler = new DrawHandler(canvasShape.Width, canvasShape.Height, canvasShape);
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

                        FileHandler.SaveDataToFile(filePath, commandsToSave);
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
                        string fileContent = FileHandler.ReadFileContent(filePath);
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

            if (!string.IsNullOrEmpty(userInput))
            {
                drawHandler.ExecuteCommand(userInput);
                canvasShape.Image = drawHandler.GetCanvasImage();
            }

            if (!string.IsNullOrEmpty(inputCommands))
            {
                drawHandler.ExecuteMultilineCommand(inputCommands);
                canvasShape.Image = drawHandler.GetCanvasImage();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            drawHandler.ClearCanvas();
            commandBox.Text = string.Empty;
            programWindow.Text = string.Empty;
        }


        private void programWindow_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    drawHandler.SetPenColor(colorDialog.Color);
                }
            }
        }

        private void canvasShape_Click(object sender, EventArgs e)
        {

        }
    }

}
