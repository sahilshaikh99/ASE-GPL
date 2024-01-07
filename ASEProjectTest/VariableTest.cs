using ASEProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProjectTest
{
    [TestClass]
        public class VariableManagerTests
        {
            [TestMethod]
            public void TestVariableExists_ExistingVariable_ReturnsTrue()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);

                var result = variableManager.VariableExists("x");

                Assert.IsTrue(result);
            }

            [TestMethod]
            public void TestVariableExists_NonExistingVariable_ReturnsFalse()
            {
                var variableManager = new VariableManager();

                var result = variableManager.VariableExists("y");

                Assert.IsFalse(result);
            }

            [TestMethod]
            public void TestGetVariableValue_ExistingVariable_ReturnsValue()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);

                var result = variableManager.GetVariableValue("x");

                Assert.AreEqual(42, result);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void TestGetVariableValue_NonExistingVariable_ThrowsArgumentException()
            {
                var variableManager = new VariableManager();

                var result = variableManager.GetVariableValue("y");

                // ArgumentException is expected
            }

            [TestMethod]
            public void TestSetVariableValue_SetVariable_ValueIsSet()
            {
                var variableManager = new VariableManager();

                variableManager.SetVariableValue("x", 42);

                Assert.AreEqual(42, variableManager.GetVariableValue("x"));
            }

            [TestMethod]
            public void TestClearVariables_ClearVariables_NoVariables()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);

                variableManager.ClearVariables();

                Assert.IsFalse(variableManager.VariableExists("x"));
            }

            [TestMethod]
            public void TestGetVariableNames_MultipleVariables_ReturnsVariableNames()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);
                variableManager.SetVariableValue("y", 10);

                var result = variableManager.GetVariableNames();

                CollectionAssert.AreEquivalent(new[] { "x", "y" }, result.ToArray());
            }

            [TestMethod]
            public void TestHandleVariableAssignment_ValidAssignment_VariableValueUpdated()
            {
                var variableManager = new VariableManager();

                variableManager.HandleVariableAssignment("x = 42");

                Assert.AreEqual(42, variableManager.GetVariableValue("x"));
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void TestHandleVariableAssignment_InvalidAssignment_ThrowsArgumentException()
            {
                var variableManager = new VariableManager();

                variableManager.HandleVariableAssignment("x = y + 42");

                // ArgumentException is expected
            }

            [TestMethod]
            public void TestEvaluateExpression_SimpleExpression_ReturnsCorrectValue()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);

                var result = variableManager.EvaluateExpression("x + 10");

                Assert.AreEqual(52, result);
            }

            [TestMethod]
            public void TestEvaluateExpression_ComplexExpression_ReturnsCorrectValue()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);
                variableManager.SetVariableValue("y", 10);

                var result = variableManager.EvaluateExpression("x + y * 2");

                Assert.AreEqual(62, result);
            }
        }
    }
