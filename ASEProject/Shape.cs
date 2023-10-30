using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    public abstract class Shape
    {
        public abstract void Draw(Graphics graphics, Color pencolor, int x, int y, int width, int height, int radius, bool fill);
    }

}
