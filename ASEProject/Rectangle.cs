using ASEProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class Rectangle: Shape
    {
        public override void Draw(Graphics graphics, Color penColor, int x, int y, int width, int height, int radius, bool fill)
        {
            if (fill)
            {
                using (Brush brush = new SolidBrush(penColor))
                {
                    graphics.FillRectangle(brush, x - width, y - height, width, height);
                }
            }
            else
            {
                using (Pen pen = new Pen(penColor))
                {
                    graphics.DrawRectangle(pen, x - width, y - height, width, height);
                }

            }
        }
    }
}
