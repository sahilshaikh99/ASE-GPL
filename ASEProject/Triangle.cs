using System;
using System.Collections.Generic;
using System.Drawing;

namespace ASEProject
{
    /// <summary>
    /// This class contains inherited method Draw to draw a triangle
    /// </summary>
    public class Triangle : Shape
    {
        /// <summary>
        /// Override the Draw method to draw a triangle
        /// </summary>
        /// <param name="graphics">This is a graphics object to perform graphical operations</param>
        /// <param name="penColor">This is the color which will be used when drawing shapes. For example: red, blue, or any other color</param>
        /// <param name="x">This is x-axis which determines the current cursor position</param>
        /// <param name="y">This is y-axis which determines the current cursor position</param>
        /// <param name="width">This is the width of the shape if defined in some commands</param>
        /// <param name="height">This is the height of the shape if defined in some commands</param>
        /// <param name="radius">This is the radius of the triangle</param>
        /// <param name="fill">Draw shapes either filled or outlined</param>
        public override void Draw(Graphics graphics, Color penColor, int x, int y, int width, int height, int radius, bool fill)
        {
            if (fill)
            {
                // Calculate vertex points for a filled triangle
                Point[] points = {
                    new Point(x - width / 2, y + height / 2),
                    new Point(x + width / 2, y + height / 2),
                    new Point(x, y - radius)
                };

                using (Brush brush = new SolidBrush(penColor))
                {
                    graphics.FillPolygon(brush, points);
                }
            }
            else
            {
                // Calculate vertex points for an outlined triangle
                Point[] points = {
                    new Point(x - width / 2, y + height / 2),
                    new Point(x + width / 2, y + height / 2),
                    new Point(x, y - radius)
                };

                using (Pen pen = new Pen(penColor))
                {
                    graphics.DrawPolygon(pen, points);
                }
            }
        }
    }
}
