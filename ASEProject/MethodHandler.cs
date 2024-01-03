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

            var match = Regex.Match(methodDefinition[0], @"method\s+(\w+)\s*\(([^)]*)\)");
            if (match.Success)
            {
                string methodName = match.Groups[1].Value;
                string parameterList = match.Groups[2].Value.Trim(); // Trim to remove leading/trailing spaces
                Console.WriteLine($"Method Name: {methodName}");
                Console.WriteLine($"Parameter List: {parameterList}");
                // Extract method body
                string methodBody = string.Join("\n", methodDefinition.Skip(1)).Trim();
                Console.WriteLine($"{methodName} {parameterList} {methodBody}");
                userDefinedMethods[methodName] = $"{parameterList} => {methodBody}";
            }
            else
            {
                var methodNameMatch = Regex.Match(methodDefinition[0], @"method\s+(\w+)");
                if (methodNameMatch.Success)
                {
                    string methodName = methodNameMatch.Groups[1].Value;
                    // Extract method body
                    string methodBody = string.Join("\n", methodDefinition.Skip(1)).Trim();
                    Console.WriteLine($"{methodName} {methodBody}");
                    userDefinedMethods[methodName] = $" => {methodBody}";
                }
                else
                {
                    exceptionMessages.Add("Error: Invalid method signature.");
                    return;
                }
            }
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

            Console.WriteLine(methodDefinition);
            // Parse parameters and method body
            string[] parameters = argumentList.Split(',').Select(p => p.Trim()).ToArray();
            string[] definitionArg = methodDefinition.Split(new[] { "=>" }, StringSplitOptions.None);
            string[] finalArg = definitionArg[0].Split(',');
            Console.WriteLine(string.Join(", ", finalArg));

            if (finalArg.Length != parameters.Length)
            {
                exceptionMessages.Add($"Error: Number of parameters in the method call does not match the method definition for '{methodName}'.");
                return;
            }

            // Extract method body
            int arrowIndex = methodDefinition.IndexOf("=>");
            if (arrowIndex == -1)
            {
                exceptionMessages.Add($"Error: Invalid method definition for '{methodName}'.");
                return;
            }

            string methodBody = methodDefinition.Substring(arrowIndex + 2).Trim();
            // Map parameters to their values
            Dictionary<string, int> parameterValues = new Dictionary<string, int>();


            if (argumentList != "")
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.WriteLine(parameters[i]);
                    string originalParameter = finalArg[i].Trim();
                    string trimmedParameter = parameters[i].Trim();
                    Console.WriteLine(originalParameter);
                    if (variableManager.VariableExists(trimmedParameter))
                    {
                        parameterValues[originalParameter] = variableManager.GetVariableValue(trimmedParameter);
                    }
                    else if (int.TryParse(trimmedParameter, out int constantValue))
                    {
                        parameterValues[originalParameter] = constantValue;
                    }
                    else
                    {
                        exceptionMessages.Add($"Error: Parameter '{trimmedParameter}' not found or not a valid constant.");
                        return;
                    }
                    // Replace method parameters with values
                    foreach (var parameter in parameterValues)
                    {
                        // Use Regex.Escape to handle special characters in the parameter
                        methodBody = Regex.Replace(methodBody, $@"\b{Regex.Escape(parameter.Key)}\b", parameter.Value.ToString());
                    }
                }
            }

            // Execute the method body as a set of commands
            string[] methodCommands = methodBody.Split('\n');
            foreach (string methodCommand in methodCommands)
            {
                drawHandler.ExecuteCommand(methodCommand.Trim());
            }
        }

    }

}
