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
        public override void Draw(Graphics graphics,int x, int y, int width, int height, int radius)
        {
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                graphics.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
            }

        }
    }
}
