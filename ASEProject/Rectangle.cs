﻿using ASEProject;
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
        public override void Draw(Graphics graphics, int x, int y, int width, int height, int radius)
        {
            using (Brush brush = new SolidBrush(Color.Red))
            {
                graphics.FillRectangle(brush, x - width, y - height, width, height);
            }
        }
    }
}
