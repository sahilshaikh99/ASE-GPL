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
        /// <param name="angle">This parameter is used for rotation of shapes.</param>

        public override void Draw(Graphics graphics, Color penColor, int cursorPosX, int cursorPosY, int width, int height, int radius, bool fill, int angle)
        {
            using (Pen pen = new Pen(penColor))
            {
                if (fill)
                {
                    using (Brush brush = new SolidBrush(penColor))
                    {
                        graphics.TranslateTransform(cursorPosX + width / 2, cursorPosY + height / 2);
                        graphics.RotateTransform(angle);
                        graphics.FillRectangle(brush, -width / 2, -height / 2, width, height);
                        graphics.ResetTransform();
                    }
                }
                else
                {
                    graphics.TranslateTransform(cursorPosX + width / 2, cursorPosY + height / 2);
                    graphics.RotateTransform(angle);
                    graphics.DrawRectangle(pen, -width / 2, -height / 2, width, height);
                    graphics.ResetTransform();
                }
            }
        }

    }
    }