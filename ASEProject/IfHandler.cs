using System;

namespace ASEProject
{
    public class IfHandler
    {
        private readonly VariableManager variableManager;

        public IfHandler(VariableManager variableManager)
        {
            this.variableManager = variableManager;
        }

        public bool ExecuteIfBlock(string condition)
        {
            // Handle if statement
            condition = condition.Trim();
            return EvaluateCondition(condition);
        }

        public bool EvaluateCondition(string condition)
        {
            var parts = condition.Split(new[] { '<', '>', '=' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new ArgumentException(new ExceptionHandler().generateException(402, "if", "valid condition"));
            }

            string leftVariableName = parts[0].Trim();
            string rightValueString = parts[1].Trim();

            // Check if the right side is a variable
            if (int.TryParse(rightValueString, out int comparisonValue))
            {
                // Right side is a numeric value
                int leftVariableValue = variableManager.GetVariableValue(leftVariableName);

                return condition.Contains("<") ? leftVariableValue < comparisonValue :
                       condition.Contains(">") ? leftVariableValue > comparisonValue :
                                                  leftVariableValue == comparisonValue;
            }
            else
            {
                // Right side is a variable
                int leftVariableValue = variableManager.GetVariableValue(leftVariableName);
                int rightVariableValue = variableManager.GetVariableValue(rightValueString);

                return condition.Contains("<") ? leftVariableValue < rightVariableValue :
                       condition.Contains(">") ? leftVariableValue > rightVariableValue :
                                                  leftVariableValue == rightVariableValue;
            }
        }
    }
}
