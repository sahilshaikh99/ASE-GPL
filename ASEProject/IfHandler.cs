using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

            string variableName = parts[0].Trim();
            string comparisonValueString = parts[1].Trim();

            if (!int.TryParse(comparisonValueString, out int comparisonValue))
            {
                throw new ArgumentException(new ExceptionHandler().generateException(402, "if", "numeric value"));
            }

            int variableValue = variableManager.GetVariableValue(variableName);

            return condition.Contains("<") ? variableValue < comparisonValue :
                   condition.Contains(">") ? variableValue > comparisonValue :
                                              variableValue == comparisonValue;
        }

    }
}
