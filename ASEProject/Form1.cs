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
  
        public Form1()
        {
            InitializeComponent();
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

            var (shapeName, x, y) = new CommandParser().ParseCommand(userInput, canvas.Width, canvas.Height);
            MessageBox.Show(shapeName);
 
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
  
        }

        private void programWindow_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
