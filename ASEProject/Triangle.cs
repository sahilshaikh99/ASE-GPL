using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    internal class Triangle: Shape
    {
        public override void Draw(Graphics graphics, int x, int y)
        {
            Point[] points = { new Point(x, y), new Point(x + 100, y), new Point(x + 50, y + 100) };
            using (Brush brush = new SolidBrush(Color.Red))
            {
                graphics.FillPolygon(brush, points);
            }
        }

    }
}
