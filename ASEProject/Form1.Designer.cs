namespace ASEProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveBtn = new System.Windows.Forms.Button();
            this.openBtn = new System.Windows.Forms.Button();
            this.programWindow = new System.Windows.Forms.RichTextBox();
            this.commandBox = new System.Windows.Forms.TextBox();
            this.runBtn = new System.Windows.Forms.Button();
            this.syntexBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.canvasShape = new System.Windows.Forms.PictureBox();
            this.penColorSelectorButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvasShape)).BeginInit();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.Location = new System.Drawing.Point(3, 1);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // openBtn
            // 
            this.openBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openBtn.Location = new System.Drawing.Point(85, 1);
            this.openBtn.Name = "openBtn";
            this.openBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.openBtn.Size = new System.Drawing.Size(75, 23);
            this.openBtn.TabIndex = 1;
            this.openBtn.Text = "Open";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // programWindow
            // 
            this.programWindow.Location = new System.Drawing.Point(13, 30);
            this.programWindow.Name = "programWindow";
            this.programWindow.Size = new System.Drawing.Size(363, 315);
            this.programWindow.TabIndex = 2;
            this.programWindow.Text = "";
            this.programWindow.TextChanged += new System.EventHandler(this.programWindow_TextChanged);
            // 
            // commandBox
            // 
            this.commandBox.Location = new System.Drawing.Point(13, 377);
            this.commandBox.Name = "commandBox";
            this.commandBox.Size = new System.Drawing.Size(362, 20);
            this.commandBox.TabIndex = 3;
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(37, 403);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(75, 23);
            this.runBtn.TabIndex = 4;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // syntexBtn
            // 
            this.syntexBtn.Location = new System.Drawing.Point(144, 403);
            this.syntexBtn.Name = "syntexBtn";
            this.syntexBtn.Size = new System.Drawing.Size(75, 23);
            this.syntexBtn.TabIndex = 5;
            this.syntexBtn.Text = "Syntex";
            this.syntexBtn.UseVisualStyleBackColor = true;
            this.syntexBtn.Click += new System.EventHandler(this.syntexBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(244, 403);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 6;
            this.clearBtn.Text = "Clear";
            this.clearBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // canvasShape
            // 
            this.canvasShape.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.canvasShape.Location = new System.Drawing.Point(395, 30);
            this.canvasShape.Name = "canvasShape";
            this.canvasShape.Size = new System.Drawing.Size(435, 315);
            this.canvasShape.TabIndex = 7;
            this.canvasShape.TabStop = false;
            this.canvasShape.Click += new System.EventHandler(this.canvasShape_Click);
            // 
            // penColorSelectorButton
            // 
            this.penColorSelectorButton.Location = new System.Drawing.Point(13, 348);
            this.penColorSelectorButton.Name = "penColorSelectorButton";
            this.penColorSelectorButton.Size = new System.Drawing.Size(75, 23);
            this.penColorSelectorButton.TabIndex = 8;
            this.penColorSelectorButton.Text = "Pen Color";
            this.penColorSelectorButton.UseVisualStyleBackColor = true;
            this.penColorSelectorButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 438);
            this.Controls.Add(this.penColorSelectorButton);
            this.Controls.Add(this.canvasShape);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.syntexBtn);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.commandBox);
            this.Controls.Add(this.programWindow);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.saveBtn);
            this.Name = "Form1";
            this.Text = "GPL";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvasShape)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.RichTextBox programWindow;
        private System.Windows.Forms.TextBox commandBox;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button syntexBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.PictureBox canvasShape;
        private System.Windows.Forms.Button penColorSelectorButton;
    }
}

