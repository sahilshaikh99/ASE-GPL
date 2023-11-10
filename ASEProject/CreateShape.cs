using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    /// <summary>
    /// This class is responsible for creating instances of different shape classes.
    /// </summary>
    class CreateShape
    {
        /// <summary>
        /// Creates and returns an instance of a shape based on the provided shape name.
        /// </summary>
        /// <param name="shapeName">Name of the shape to be created.</param>
        /// <returns>An instance of a shape or null if the shape name is unknown.</returns>
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
                case "drawto":
                    return new DrawTo();
                default:
                    return null;
            }
        }
    }
}
