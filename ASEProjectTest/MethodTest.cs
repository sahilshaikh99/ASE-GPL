using ASEProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for the MethodHandler class.
    /// </summary>
    [TestClass]
        public class MethodHandlerTests
        {
        /// <summary>
        /// Tests the definition of a valid method.
        /// </summary>
        [TestMethod]
        public void TestDefineMethod_ValidMethodDefinition()
        {
            DrawHandler drawHandler = new DrawHandler(800, 600, null);
            VariableManager variableManager = new VariableManager();
            List<string> exceptionMessages = new List<string>();
            MethodHandler methodHandler = new MethodHandler(drawHandler, variableManager, exceptionMessages);

            methodHandler.DefineMethod(new List<string> { "method TestMethod (parameterA, parameterB)", "rectangle 10 10" });

            Assert.AreEqual(0, exceptionMessages.Count);
            Assert.IsTrue(methodHandler.IsMethodDefined("TestMethod"));
        }

        /// <summary>
        /// Tests the definition of a method with an invalid signature.
        /// </summary>
        [TestMethod]
            public void TestDefineMethod_InvalidMethodSignature()
            {
                DrawHandler drawHandler = new DrawHandler(800, 600, null);
                VariableManager variableManager = new VariableManager();
                List<string> exceptionMessages = new List<string>();
                MethodHandler methodHandler = new MethodHandler(drawHandler, variableManager, exceptionMessages);

                methodHandler.DefineMethod(new List<string> { "invalid signature", "draw rectangle 10 10 20 30" });

                Assert.AreEqual(1, exceptionMessages.Count);
                Assert.IsFalse(methodHandler.IsMethodDefined("TestMethod"));
            }

        /// <summary>
        /// Tests calling an undefined method.
        /// </summary>
        [TestMethod]
            public void TestCallMethod_UndefinedMethod()
            {
                DrawHandler drawHandler = new DrawHandler(800, 600, null);
                VariableManager variableManager = new VariableManager();
                List<string> exceptionMessages = new List<string>();
                MethodHandler methodHandler = new MethodHandler(drawHandler, variableManager, exceptionMessages);

                methodHandler.CallMethod("UndefinedMethod (5, 10)");

                Assert.AreEqual(1, exceptionMessages.Count);
            }

        /// <summary>
        /// Tests calling a method with an invalid method call.
        /// </summary>
        [TestMethod]
            public void TestCallMethod_InvalidMethodCall()
            {
                DrawHandler drawHandler = new DrawHandler(800, 600, null);
                VariableManager variableManager = new VariableManager();
                List<string> exceptionMessages = new List<string>();
                MethodHandler methodHandler = new MethodHandler(drawHandler, variableManager, exceptionMessages);
                methodHandler.DefineMethod(new List<string> { "method TestMethod (parameterA, parameterB)", "draw rectangle 10 10 20 30" });

                methodHandler.CallMethod("InvalidMethodCall");

                Assert.AreEqual(1, exceptionMessages.Count);
            }
        }
    }

