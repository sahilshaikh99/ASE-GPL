using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
     class CreateShape
    {
        public Shape MakeShape(string shapeName)
        {
            switch (shapeName)
            {
                case "circle":
                    return new Circle();
                case "rectangle":
                    return new Rectangle();
                case "triangle":
                    return new Triangle();
                default:
                    return null;
            }
        }
    }
}
