using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ASEProject
{
    /// <summary>
    /// Manages variables, their values, and handles variable assignments and expressions.
    /// </summary>
    public class VariableManager
    {
        private static VariableManager instance;

        private Dictionary<string, int> variables = new Dictionary<string, int>();

        /// <summary>
        /// Initializes a new instance of the VariableManager class.
        /// </summary>
        public VariableManager() { }

        /// <summary>
        /// Gets the singleton instance of the VariableManager.
        /// </summary>
        public static VariableManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VariableManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Checks if a variable with the specified name exists.
        /// </summary>
        /// <param name="variableName">The name of the variable to check.</param>
        /// <returns>True if the variable exists, otherwise false.</returns>
        public bool VariableExists(string variableName)
        {
            return variables.ContainsKey(variableName);
        }

        /// <summary>
        /// Gets the value of a variable with the specified name.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <returns>The value of the variable.</returns>
        public int GetVariableValue(string variableName)
        {
            if (variables.TryGetValue(variableName, out int value))
            {
                return value;
            }
            else
            {
                throw new ArgumentException($"Variable '{variableName}' not found.");
            }
        }

        /// <summary>
        /// Sets the value of a variable with the specified name.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="value">The value to set.</param>
        public void SetVariableValue(string variableName, int value)
        {
            variables[variableName] = value;
        }

        /// <summary>
        /// Clears all variables from the VariableManager.
        /// </summary>
        public void ClearVariables()
        {
            variables.Clear();
        }

        /// <summary>
        /// Gets the names of all variables.
        /// </summary>
        /// <returns>An IEnumerable containing the names of all variables.</returns>
        public IEnumerable<string> GetVariableNames()
        {
            return variables.Keys;
        }

        /// <summary>
        /// Handles a variable assignment command, updating the variable value.
        /// </summary>
        /// <param name="command">The variable assignment command.</param>
        internal void HandleVariableAssignment(string command)
        {
            // Split the command by '=' and remove extra spaces
            string[] assignmentParts = command.Split('=').Select(part => part.Trim()).ToArray();

            if (assignmentParts.Length == 2)
            {
                string variableName = assignmentParts[0];
                string expression = assignmentParts[1];
                // Check if the variable exists
                if (VariableExists(variableName))
                {
                    // Evaluate the expression and update the variable value
                    int result = EvaluateExpression(expression);
                    SetVariableValue(variableName, result);
                }
                else
                {
                    // If the variable doesn't exist, create it
                    int result = EvaluateExpression(expression);
                    SetVariableValue(variableName, result);
                }
            }

            //                    throw new ArgumentException(new ExceptionHandler().generateException(402, "variable", "numeric value."));
            else
            {
                throw new ArgumentException(new ExceptionHandler().generateException(402, "variable", "valid variable assignment command."));
            }
        }


        /// <summary>
        /// Evaluates a numeric expression and returns the result.
        /// </summary>
        /// <param name="expression">The numeric expression to evaluate.</param>
        /// <returns>The result of the numeric expression evaluation.</returns>
        internal int EvaluateExpression(string expression)
        {
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            foreach (var variableName in GetVariableNames())
            {
                variableValues[variableName] = GetVariableValue(variableName);
            }

            return EvaluateExpressionRecursive(expression, variableValues);
        }

        /// <summary>
        /// Recursively evaluates a numeric expression using variable values.
        /// </summary>
        /// <param name="expression">The numeric expression to evaluate.</param>
        /// <param name="variableValues">A dictionary containing variable names and their values.</param>
        /// <returns>The result of the numeric expression evaluation.</returns>
        internal int EvaluateExpressionRecursive(string expression, Dictionary<string, int> variableValues)
        {
            if (int.TryParse(expression, out int value))
            {
                return value;
            }

            foreach (var variableName in variableValues.Keys)
            {
                string variableExpression = $"{variableName}";

                if (expression.Contains(variableExpression))
                {
                    expression = expression.Replace(variableExpression, variableValues[variableName].ToString());
                }
            }

            DataTable table = new DataTable();
            var result = table.Compute(expression, "");
            return Convert.ToInt32(result);
        }
    }
}
