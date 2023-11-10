using ASEProject;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ASEProject
{
    /// <summary>
    /// This class contains inherited method Draw to draw a rectangle
    /// </summary>
    public class Rectangle : Shape
    {
        /// <summary>
        /// Override the Draw method to draw a rectangle
        /// </summary>
        /// <param name="graphics">This is a graphics object to perform graphical operations</param>
        /// <param name="penColor">This is the color which will be used when drawing shapes. For example: red, blue, or any other color</param>
        /// <param name="x">This is x-axis which determines the current cursor position</param>
        /// <param name="y">This is y-axis which determines the current cursor position</param>
        /// <param name="width">This is the width of the shape if defined in some commands</param>
        /// <param name="height">This is the height of the shape if defined in some commands</param>
        /// <param name="radius">This is not applicable for a rectangle</param>
        /// <param name="fill">Draw shapes either filled or outlined</param>
        public override void Draw(Graphics graphics, Color penColor, int x, int y, int width, int height, int radius, bool fill)
        {
            // Calculate the top-left corner coordinates for the rectangle
            int topLeftX = x - width / 2;
            int topLeftY = y - height / 2;

            if (fill)
            {
                // Draw a filled rectangle
                using (Brush brush = new SolidBrush(penColor))
                {
                    graphics.FillRectangle(brush, topLeftX, topLeftY, width, height);
                }
            }
            else
            {
                // Draw an outlined rectangle
                using (Pen pen = new Pen(penColor))
                {
                    graphics.DrawRectangle(pen, topLeftX, topLeftY, width, height);
                }
            }
        }
    }
}
