using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class Circle: Shape
    {
        public override void Draw(Graphics graphics, Color penColor, int x, int y, int width, int height, int radius, bool fill)
        {
            if (fill)
            {
                using (Brush brush = new SolidBrush(penColor))
                {
                    graphics.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
                }
            }
            else
            {
                using (Pen pen = new Pen(penColor))
                {
                    graphics.DrawEllipse(pen, x - radius, y - radius, 2 * radius, 2 * radius);
                }
            }

        }
    }
}
