using System;
using System.Collections.Generic;
using System.Drawing;

namespace ASEProject
{
    /// <summary>
    /// This is an abstract base class for different shapes.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Abstract method to be implemented by derived classes for drawing shapes.
        /// </summary>
        /// <param name="graphics">Graphics object for performing graphical operations.</param>
        /// <param name="pencolor">Color to be used when drawing shapes.</param>
        /// <param name="x">X-axis coordinate determining the current cursor position.</param>
        /// <param name="y">Y-axis coordinate determining the current cursor position.</param>
        /// <param name="width">Width of the shape if defined in some commands.</param>
        /// <param name="height">Height of the shape if defined in some commands.</param>
        /// <param name="radius">Radius of the shape if applicable (e.g., for circles).</param>
        /// <param name="fill">Determines whether to draw shapes filled or outlined.</param>
        public abstract void Draw(Graphics graphics, Color pencolor, int x, int y, int width, int height, int radius, bool fill);
    }
}
