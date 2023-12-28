using System;
using System.Collections.Generic;

namespace ASEProject
{
    public class VariableManager
    {
        private static VariableManager instance;

        private Dictionary<string, int> variables = new Dictionary<string, int>();

        private VariableManager() { }

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

        public bool VariableExists(string variableName)
        {
            return variables.ContainsKey(variableName);
        }

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

        public void SetVariableValue(string variableName, int value)
        {
            variables[variableName] = value;
        }

        public void ClearVariables()
        {
            variables.Clear();
        }

        public IEnumerable<string> GetVariableNames()
        {
            return variables.Keys;
        }
    }
}
