using ASEProject;
using System;

public class WhileHandler
{
    private readonly VariableManager variableManager;

    public WhileHandler(VariableManager variableManager)
    {
        this.variableManager = variableManager;
    }

    public bool HandleWhileLoop(string condition)
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