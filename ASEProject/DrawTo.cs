using ASEProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    /// <summary>
    /// This class represents the 'drawto' shape, which draws a line from one point to another.
    /// </summary>
    public class DrawTo : Shape
    {
        /// <summary>
        /// Draws a line from one point to another on the canvas.
        /// </summary>
        /// <param name="graphics">The graphics object for performing graphical operations.</param>
        /// <param name="penColor">The color used when drawing the line.</param>
        /// <param name="x">The x-coordinate of the starting point of the line.</param>
        /// <param name="y">The y-coordinate of the starting point of the line.</param>
        /// <param name="width">The x-coordinate of the ending point of the line.</param>
        /// <param name="height">The y-coordinate of the ending point of the line.</param>
        /// <param name="radius">This parameter is not used for drawing a line.</param>
        /// <param name="fill">This parameter is not used for drawing a line.</param>
        public override void Draw(Graphics graphics, Color penColor, int x, int y, int width, int height, int radius, bool fill)
        {
            // Draw a line from the starting point (x, y) to the ending point (width, height)
            graphics.DrawLine(new Pen(penColor), x, y, width, height);
        }
    }
}
