﻿using System;
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
        private int cursorPosX = 0;
        private int cursorPosY = 0;
        private Color penColor = Color.Black;
        private bool fillShapes = true; 
        private List<Shape> myShapes = new List<Shape>();

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

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string userInput = commandBox.Text;


            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                var (shapeName, x, y, width, height, radius, penColorName, fill) = new CommandParser().ParseCommand(userInput, canvasShape.Width, canvasShape.Height);
                if (shapeName != null)
                {
                    if (shapeName == "moveto")
                    {
                        cursorPosX = x;
                        cursorPosY = y;
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
                        cursorPosX = 0;
                        cursorPosY = 0;
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

                            shape.Draw(graphics, penColor, cursorPosX, cursorPosY, width, height, radius, fillShapes);
                        }

                        else
                        {
                            MessageBox.Show("Unknown shape.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid command or coordinates are out of bounds.");
                }

            }
            canvasShape.Image = (Image)canvasBitmap.Clone();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
 
            canvasBitmap = new Bitmap(canvasShape.Width, canvasShape.Height);

            canvasShape.Image = canvasBitmap;

            commandBox.Text = string.Empty;
            programWindow.Text = string.Empty;
        }

        private void ClearMyCanvas()
        {
            myShapes.Clear();
            canvasShape.Image = null;
        }

        private void programWindow_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
