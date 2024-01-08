using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ASEProject
{
    /// <summary>
    /// Handles user-defined methods, their definition, and execution.
    /// </summary>
    internal class MethodHandler
    {
        private Dictionary<string, string> userDefinedMethods = new Dictionary<string, string>();
        private VariableManager variableManager;
        private DrawHandler drawHandler;
        private List<string> exceptionMessages;

        /// <summary>
        /// Initializes a new instance of the MethodHandler class.
        /// </summary>
        /// <param name="drawHandler">The DrawHandler instance for handling drawing commands.</param>
        /// <param name="variableManager">The VariableManager instance for managing variables.</param>
        /// <param name="exceptionMessages">The list to store exception messages.</param>
        public MethodHandler(DrawHandler drawHandler, VariableManager variableManager, List<string> exceptionMessages)
        {
            this.variableManager = variableManager;
            this.drawHandler = drawHandler;
            this.exceptionMessages = exceptionMessages;
        }

        /// <summary>
        /// Defines a user-defined method based on the provided method definition.
        /// </summary>
        /// <param name="methodDefinition">The list of strings representing the method definition.</param>
        public void DefineMethod(List<string> methodDefinition)
        {
            Console.WriteLine(string.Join(", ", methodDefinition));

            if (methodDefinition.Count < 2)
            {
                exceptionMessages.Add("Error: Incomplete method definition.");
                return;
            }

            var match = Regex.Match(methodDefinition[0], @"method\s+(\w+)\s*\(([^)]*)\)");
            if (match.Success)
            {
                string methodName = match.Groups[1].Value;
                string parameterList = match.Groups[2].Value.Trim(); 
        
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

        /// <summary>
        /// Calls a user-defined method based on the provided method call.
        /// </summary>
        /// <param name="methodCall">The method call to execute.</param>
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

        /// <summary>
        /// Executes a user-defined method with the provided name, argument list, and method definition.
        /// </summary>
        /// <param name="methodName">The name of the method to execute.</param>
        /// <param name="argumentList">The argument list for the method call.</param>
        /// <param name="methodDefinition">The method definition containing parameters and method body.</param>
        private void ExecuteMethod(string methodName, string argumentList, string methodDefinition)
        {
            Console.WriteLine(methodName + " " + argumentList);

            Console.WriteLine(methodDefinition);
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

        public bool IsMethodDefined(string methodName)
        {
            return userDefinedMethods.ContainsKey(methodName);
        }
    }

}
