using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ASEProject
{
    internal class MethodHandler
    {
        private Dictionary<string, string> userDefinedMethods = new Dictionary<string, string>();
        private VariableManager variableManager;
        private DrawHandler drawHandler;
        private List<string> exceptionMessages;

        public MethodHandler(DrawHandler drawHandler, VariableManager variableManager, List<string> exceptionMessages)
        {
            this.variableManager = variableManager;
            this.drawHandler = drawHandler;
            this.exceptionMessages = exceptionMessages;
        }

        public void DefineMethod(List<string> methodDefinition)
        {
            Console.WriteLine(string.Join(", ", methodDefinition));

            // Check if the method has at least two lines (signature and body)
            if (methodDefinition.Count < 2)
            {
                exceptionMessages.Add("Error: Incomplete method definition.");
                return;
            }

            // Extract method signature
            string[] parts = methodDefinition[0].Split(' ');
            string methodName = parts[1];
            string parameterList = parts[2].Trim('(', ')');

            // Extract method body
            string methodBody = string.Join("\n", methodDefinition.Skip(1)).Trim();
            Console.WriteLine($"{methodName} {parameterList} {methodBody}");
            userDefinedMethods[methodName] = $"{parameterList} => {methodBody}";
        }

        public void CallMethod(string methodCall)
        {
            int openParenIndex = methodCall.IndexOf('(');
            int closeParenIndex = methodCall.IndexOf(')');

            if (openParenIndex != -1 && closeParenIndex != -1)
            {
                string methodName = methodCall.Substring(0, openParenIndex).Trim();
                string argumentList = methodCall.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1).Trim();

                if (userDefinedMethods.TryGetValue(methodName, out string methodDefinition))
                {
                    Console.WriteLine("jkjnjf");
                    ExecuteMethod(methodName, argumentList, methodDefinition);
                }
                else
                {
                    exceptionMessages.Add($"Error: Method '{methodName}' not defined.");
                }
            }
        }

        private void ExecuteMethod(string methodName, string argumentList, string methodDefinition)
        {
            Console.WriteLine(methodName + " " + argumentList);

            // Parse parameters and method body
            string[] parameters = argumentList.Split(',');

            // Map parameters to their values
            Dictionary<string, int> parameterValues = new Dictionary<string, int>();
            for (int i = 0; i < parameters.Length; i++)
            {
                string parameter = parameters[i].Trim();
                if (variableManager.VariableExists(parameter))
                {
                    parameterValues[parameter] = variableManager.GetVariableValue(parameter);
                }
                else if (int.TryParse(parameter, out int constantValue))
                {
                    parameterValues[parameter] = constantValue;
                }
                else
                {
                    exceptionMessages.Add($"Error: Parameter '{parameter}' not found or not a valid constant.");
                    return;
                }
            }

            // Extract method body
            int arrowIndex = methodDefinition.IndexOf("=>");
            if (arrowIndex == -1)
            {
                exceptionMessages.Add($"Error: Invalid method definition for '{methodName}'.");
                return;
            }

            string methodBody = methodDefinition.Substring(arrowIndex + 2).Trim();

            // Replace method parameters with values
            foreach (var parameter in parameterValues.Keys)
            {
                // Ensure to replace only whole words, not substrings
                methodBody = Regex.Replace(methodBody, $@"\b{parameter}\b", parameterValues[parameter].ToString());
            }

            // Execute the method body as a set of commands
            string[] methodCommands = methodBody.Split('\n');
            foreach (string methodCommand in methodCommands)
            {
                drawHandler.ExecuteCommand(methodCommand);
            }
        }


    }
}
