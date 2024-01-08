using ASEProject;
using System;

/// <summary>
/// Handles the evaluation of conditions in while loops.
/// </summary>
public class WhileHandler
{
    private readonly VariableManager variableManager;

    /// <summary>
    /// Initializes a new instance of the WhileHandler class.
    /// </summary>
    /// <param name="variableManager">The VariableManager instance for managing variables.</param>
    public WhileHandler(VariableManager variableManager)
    {
        this.variableManager = variableManager;
    }

    /// <summary>
    /// Handles the execution of a while loop based on the specified condition.
    /// </summary>
    /// <param name="condition">The condition to evaluate for the while loop.</param>
    /// <returns>True if the condition is satisfied, indicating the while loop should continue; otherwise, false.</returns>
    public bool HandleWhileLoop(string condition)
    {
        // Handle if statement
        condition = condition.Trim();
        return EvaluateCondition(condition);
    }

    /// <summary>
    /// Evaluates the specified condition for the while loop.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <returns>True if the condition is satisfied, otherwise false.</returns>
    public bool EvaluateCondition(string condition)
    {
        var parts = condition.Split(new[] { '<', '>', '=' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
        {
            throw new ArgumentException(new ExceptionHandler().generateException(402, "while", "invalid condition format"));
        }

        string variableName = parts[0].Trim();
        string comparisonValueString = parts[1].Trim();

        if (!int.TryParse(comparisonValueString, out int comparisonValue))
        {
            throw new ArgumentException(new ExceptionHandler().generateException(402, "while", "numeric value"));
        }

        int variableValue = variableManager.GetVariableValue(variableName);

        return condition.Contains("<") ? variableValue < comparisonValue :
                   condition.Contains(">") ? variableValue > comparisonValue :
                                              variableValue == comparisonValue;
    }
}