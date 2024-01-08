using ASEProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProjectTest
{
        [TestClass]
        public class FactoryTest
        {
            [TestMethod]
            public void MakeShape_ValidCircleName_ReturnsCircleInstance()
            {
                CreateShape shapeFactory = new CreateShape();

                Shape shape = shapeFactory.MakeShape("circle");

                Assert.IsInstanceOfType(shape, typeof(Circle));
            }

            [TestMethod]
            public void MakeShape_ValidRectangleName_ReturnsRectangleInstance()
            {
                CreateShape shapeFactory = new CreateShape();

                Shape shape = shapeFactory.MakeShape("rectangle");

                Assert.IsInstanceOfType(shape, typeof(Rectangle));
            }

            [TestMethod]
            public void MakeShape_ValidTriangleName_ReturnsTriangleInstance()
            {
                CreateShape shapeFactory = new CreateShape();

                Shape shape = shapeFactory.MakeShape("triangle");

                Assert.IsInstanceOfType(shape, typeof(Triangle));
            }

            [TestMethod]
            public void MakeShape_ValidDrawToName_ReturnsDrawToInstance()
            {
                CreateShape shapeFactory = new CreateShape();

                Shape shape = shapeFactory.MakeShape("drawto");

                Assert.IsInstanceOfType(shape, typeof(DrawTo));
            }

            [TestMethod]
            public void MakeShape_UnknownShapeName_ReturnsNull()
            {
                CreateShape shapeFactory = new CreateShape();

                Shape shape = shapeFactory.MakeShape("unknownShape");

                Assert.IsNull(shape);
            }
        }
    }
