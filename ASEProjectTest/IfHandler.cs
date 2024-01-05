using ASEProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProjectTest
{
  [TestClass]
    public class IfHandlerTests
    {
        [TestMethod]
        public void TestExecuteIfBlock_NumericComparison()
        {
            VariableManager variableManager = new VariableManager();
            IfHandler ifHandler = new IfHandler(variableManager);
            variableManager.SetVariableValue("variableA", 5);

            bool result = ifHandler.ExecuteIfBlock("variableA > 3");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExecuteIfBlock_VariableComparison()
        {
            VariableManager variableManager = new VariableManager();
            IfHandler ifHandler = new IfHandler(variableManager);
            variableManager.SetVariableValue("variableA", 5);
            variableManager.SetVariableValue("variableB", 3);

            bool result = ifHandler.ExecuteIfBlock("variableA > variableB");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEvaluateCondition_NumericComparison()
        {
            VariableManager variableManager = new VariableManager();
            IfHandler ifHandler = new IfHandler(variableManager);
            variableManager.SetVariableValue("variableA", 5);

            bool result = ifHandler.EvaluateCondition("variableA > 3");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEvaluateCondition_VariableComparison()
        {
            VariableManager variableManager = new VariableManager();
            IfHandler ifHandler = new IfHandler(variableManager);
            variableManager.SetVariableValue("variableA", 5);
            variableManager.SetVariableValue("variableB", 3);

            bool result = ifHandler.EvaluateCondition("variableA > variableB");

            Assert.IsTrue(result);
        }
    }
}
