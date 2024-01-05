using ASEProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProjectTest
{
        [TestClass]
        public class WhileHandlerTests
        {
            [TestMethod]
            public void TestHandleWhileLoop_NumericComparison_True()
            {
                VariableManager variableManager = new VariableManager();
                WhileHandler whileHandler = new WhileHandler(variableManager);
                variableManager.SetVariableValue("variableA", 3);

                bool result = whileHandler.HandleWhileLoop("variableA < 5");

                Assert.IsTrue(result);
            }

            [TestMethod]
            public void TestHandleWhileLoop_NumericComparison_False()
            {
                VariableManager variableManager = new VariableManager();
                WhileHandler whileHandler = new WhileHandler(variableManager);
                variableManager.SetVariableValue("variableA", 7);

                bool result = whileHandler.HandleWhileLoop("variableA < 5");

                Assert.IsFalse(result);
            }

            [TestMethod]
            public void TestHandleWhileLoop_InvalidCondition()
            {
                VariableManager variableManager = new VariableManager();
                WhileHandler whileHandler = new WhileHandler(variableManager);

                Assert.ThrowsException<ArgumentException>(() => whileHandler.HandleWhileLoop("invalid condition"));
            }

            [TestMethod]
            public void TestEvaluateCondition_NumericComparison_True()
            {
                
                VariableManager variableManager = new VariableManager();
                WhileHandler whileHandler = new WhileHandler(variableManager);
                variableManager.SetVariableValue("variableA", 3);

                bool result = whileHandler.EvaluateCondition("variableA < 5");

                Assert.IsTrue(result);
            }

            [TestMethod]
            public void TestEvaluateCondition_NumericComparison_False()
            {
                VariableManager variableManager = new VariableManager();
                WhileHandler whileHandler = new WhileHandler(variableManager);
                variableManager.SetVariableValue("variableA", 7);

                bool result = whileHandler.EvaluateCondition("variableA < 5");

                Assert.IsFalse(result);
            }

            [TestMethod]
            public void TestEvaluateCondition_InvalidCondition()
            {
                VariableManager variableManager = new VariableManager();
                WhileHandler whileHandler = new WhileHandler(variableManager);

                Assert.ThrowsException<ArgumentException>(() => whileHandler.EvaluateCondition("invalid condition"));
            }
        }
    }
