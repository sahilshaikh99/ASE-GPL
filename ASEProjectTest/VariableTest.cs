using ASEProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProjectTest
{
    /// <summary>
    /// Test class for the VariableManager class.
    /// </summary>
    [TestClass]
        public class VariableManagerTests
        {
        /// <summary>
        /// Tests the existence check for an existing variable.
        /// </summary>
        [TestMethod]
            public void TestVariableExists_ExistingVariable_ReturnsTrue()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);

                var result = variableManager.VariableExists("x");

                Assert.IsTrue(result);
            }

        /// <summary>
        /// Tests the existence check for a non-existing variable.
        /// </summary>
        [TestMethod]
            public void TestVariableExists_NonExistingVariable_ReturnsFalse()
            {
                var variableManager = new VariableManager();

                var result = variableManager.VariableExists("y");

                Assert.IsFalse(result);
            }

        /// <summary>
        /// Tests retrieving the value of an existing variable.
        /// </summary>
        [TestMethod]
            public void TestGetVariableValue_ExistingVariable_ReturnsValue()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);

                var result = variableManager.GetVariableValue("x");

                Assert.AreEqual(42, result);
            }

        /// <summary>
        /// Tests retrieving the value of a non-existing variable, expecting an exception.
        /// </summary>
        [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void TestGetVariableValue_NonExistingVariable_ThrowsArgumentException()
            {
                var variableManager = new VariableManager();

                var result = variableManager.GetVariableValue("y");

                // ArgumentException is expected
            }

        /// <summary>
        /// Tests setting the value of a variable.
        /// </summary>
        [TestMethod]
            public void TestSetVariableValue_SetVariable_ValueIsSet()
            {
                var variableManager = new VariableManager();

                variableManager.SetVariableValue("x", 42);

                Assert.AreEqual(42, variableManager.GetVariableValue("x"));
            }

        /// <summary>
        /// Tests clearing variables and verifying that no variables exist.
        /// </summary>
        [TestMethod]
            public void TestClearVariables_ClearVariables_NoVariables()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);

                variableManager.ClearVariables();

                Assert.IsFalse(variableManager.VariableExists("x"));
            }

        /// <summary>
        /// Tests retrieving variable names when multiple variables are present.
        /// </summary>
        [TestMethod]
            public void TestGetVariableNames_MultipleVariables_ReturnsVariableNames()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);
                variableManager.SetVariableValue("y", 10);

                var result = variableManager.GetVariableNames();

                CollectionAssert.AreEquivalent(new[] { "x", "y" }, result.ToArray());
            }

        /// <summary>
        /// Tests handling a valid variable assignment and updating the variable value.
        /// </summary>
        [TestMethod]
            public void TestHandleVariableAssignment_ValidAssignment_VariableValueUpdated()
            {
                var variableManager = new VariableManager();

                variableManager.HandleVariableAssignment("x = 42");

                Assert.AreEqual(42, variableManager.GetVariableValue("x"));
            }

        /// <summary>
        /// Tests handling an invalid variable assignment, expecting an exception.
        /// </summary>
        [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void TestHandleVariableAssignment_InvalidAssignment_ThrowsArgumentException()
            {
                var variableManager = new VariableManager();

                variableManager.HandleVariableAssignment("x = y + 42");

                // ArgumentException is expected
            }

        /// <summary>
        /// Tests evaluating a simple expression and returning the correct value.
        /// </summary>
        [TestMethod]
            public void TestEvaluateExpression_SimpleExpression_ReturnsCorrectValue()
            {
                var variableManager = new VariableManager();
                variableManager.SetVariableValue("x", 42);

                var result = variableManager.EvaluateExpression("x + 10");

                Assert.AreEqual(52, result);
            }

        /// <summary>
        /// Tests evaluating a complex expression and returning the correct value.
        /// </summary>
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
