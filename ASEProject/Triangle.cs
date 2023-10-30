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
        public override void Draw(Graphics graphics, Color pencolor,  int x, int y, int width, int height, int radius)
        {
            Point[] points = { new Point(x, y), new Point(x + 100, y), new Point(x + 50, y + 100) };
            using (Brush brush = new SolidBrush(pencolor))
            {
                graphics.FillPolygon(brush, points);
            }
        }

    }
}
