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
        /// <param name="angle">Rotation angle</param>
        public override void Draw(Graphics graphics, Color penColor, int x, int y, int width, int height, int radius, bool fill, int angle)
        {
            if (fill)
            {
                Point[] points = {
                    new Point(x - width / 2, y + height / 2),
                    new Point(x + width / 2, y + height / 2),
                    new Point(x, y - radius)
                };

                RotatePoints(points, new Point(x, y), angle);

                using (Brush brush = new SolidBrush(penColor))
                {
                    graphics.FillPolygon(brush, points);
                }
            }
            else
            {
                Point[] points = {
                    new Point(x - width / 2, y + height / 2),
                    new Point(x + width / 2, y + height / 2),
                    new Point(x, y - radius)
                };

                RotatePoints(points, new Point(x, y), angle);

                using (Pen pen = new Pen(penColor))
                {
                    graphics.DrawPolygon(pen, points);
                }
            }
        }

        // Helper method to rotate points around a pivot point
        private void RotatePoints(Point[] points, Point pivot, int angle)
        {
            double angleRad = angle * (Math.PI / 180.0);

            for (int i = 0; i < points.Length; i++)
            {
                int dx = points[i].X - pivot.X;
                int dy = points[i].Y - pivot.Y;

                points[i].X = (int)(pivot.X + dx * Math.Cos(angleRad) - dy * Math.Sin(angleRad));
                points[i].Y = (int)(pivot.Y + dx * Math.Sin(angleRad) + dy * Math.Cos(angleRad));
            }
        }
    }
}
