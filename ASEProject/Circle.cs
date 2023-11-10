using System;
using System.Collections.Generic;
using System.Drawing;

namespace ASEProject
{
    /// <summary>
    /// This class conatains inherited method draw to draw circle
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// Override the Draw method to draw a circle
        /// </summary>
        /// <param name="graphics">This is graphics object to perform graphical operations</param>
        /// <param name="penColor">This is the color which will being used when drawing shapes. For example: red, blue or anyother color</param>
        /// <param name="x">This is x axis which determines current cursor position</param>
        /// <param name="y">This is y axis which determines current cursor position</param>
        /// <param name="width">This is the width of the shape if defined in some commands</param>
        /// <param name="height">This is the height of the shape if defined in some commands</param>
        /// <param name="radius">This is radius of circle</param>
        /// <param name="fill">Draw shapes either filled or outlined</param>
        public override void Draw(Graphics graphics, Color penColor, int x, int y, int width, int height, int radius, bool fill)
        {
            // Check if the circle should be filled or outlined
            if (fill)
            {
                // Draw a filled circle
                using (Brush brush = new SolidBrush(penColor))
                {
                    // Draw the ellipse using the filled
                    graphics.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
                }
            }
            else
            {
                // Draw an outlined circle
                using (Pen pen = new Pen(penColor))
                {
                    // Draw the ellipse using the outlined 
                    graphics.DrawEllipse(pen, x - radius, y - radius, 2 * radius, 2 * radius);
                }
            }
        }
    }
}
