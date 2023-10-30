using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public class Triangle: Shape
    {
        public override void Draw(Graphics graphics, Color penColor,  int x, int y, int width, int height, int radius, bool fill)
        {
            if (fill)
            {
                Point[] points = { new Point(x, y), new Point(x + 100, y), new Point(x + 50, y + 100) };
                using (Brush brush = new SolidBrush(penColor))
                {
                    graphics.FillPolygon(brush, points);
                }
            }
            else
            {
                using (Pen pen = new Pen(penColor))
                {
                    Point[] points = { new Point(x, y - height), new Point(x + width, y), new Point(x - width, y) };
                    graphics.DrawPolygon(pen, points);
                }
            }
        }

    }
}
