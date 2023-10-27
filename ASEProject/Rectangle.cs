using ASEProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    internal class Rectangle: Shape
    {
        public override void Draw(Graphics graphics, int x, int y)
        {
            using (Brush brush = new SolidBrush(Color.Red))
            {
                graphics.FillRectangle(brush, x, y, 150, 100);
            }
        }
    }
}
