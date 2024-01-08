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
    /// <summary>
    /// The main form class that represents the user interface.
    /// </summary>
    public partial class Form1 : Form
    {
        private DrawHandler drawHandler;      // Manages drawing on the canvas
        private FileHandler FileHandler = new FileHandler(); // Handles file operations
        private SyntexCheck syntexCheck;      // Checks syntax and display errors on the canvas

        /// <summary>
        /// Constructor for the main form.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // Initialize DrawHandler and SyntexCheck with canvas dimensions
            drawHandler = new DrawHandler(canvasShape.Width, canvasShape.Height, canvasShape);
            syntexCheck = new SyntexCheck(canvasShape.Width, canvasShape.Height, canvasShape);
        }

        // Event handler for Save button
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

                        // Save commands to file using FileHandler
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

        // Event handler for Open button
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

        // Event handler for Execute button
        private void button3_Click(object sender, EventArgs e)
        {
            string userInput = commandBox.Text;
            string inputCommands = programWindow.Text;

            // Execute single command
            if (!string.IsNullOrEmpty(userInput))
            {
                drawHandler.ExecuteSingleCommand(userInput);
                canvasShape.Image = drawHandler.GetCanvasImage();
            }

            // Execute multiple commands
            if (!string.IsNullOrEmpty(inputCommands))
            {
                drawHandler.ExecuteMultilineCommand(inputCommands);
                canvasShape.Image = drawHandler.GetCanvasImage();
            }
        }

        // Event handler for Clear button
        private void clearBtn_Click(object sender, EventArgs e)
        {
            drawHandler.ClearCanvas();
            commandBox.Text = string.Empty;
            programWindow.Text = string.Empty;
        }

        // Event handler for Set Pen Color button
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

        // Event handler for Syntax Check button
        private void syntexBtn_Click(object sender, EventArgs e)
        {
            string userInput = commandBox.Text;
            string inputCommands = programWindow.Text;

            // Execute syntax check for single command
            if (!string.IsNullOrEmpty(userInput))
            {
                syntexCheck.ExecuteSyntaxCheck(userInput);
            }

            // Execute syntax check for multiple commands
            if (!string.IsNullOrEmpty(inputCommands))
            {
                syntexCheck.ExecuteSyntaxCheck(inputCommands);
            }
            canvasShape.Image = syntexCheck.GetCanvasImage();
        }

        private void canvasShape_Click(object sender, EventArgs e)
        {

        }

        private void programWindow_TextChanged(object sender, EventArgs e)
        {

        }

        public void multilinecommandTest(string inputCommands)
        {
            drawHandler.ExecuteMultilineCommand(inputCommands);
            canvasShape.Image = drawHandler.GetCanvasImage();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
