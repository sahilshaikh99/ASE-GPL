using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ASEProject
{
    /// <summary>
    /// This class manages drawing shapes on a canvas, handling commands and maintaining state.
    /// </summary>
    public class DrawHandler
    {
        private Bitmap canvasBitmap;
        private Color penColor = Color.Black;
        private bool fillShapes = false;
        private List<Shape> myShapes = new List<Shape>();
        private CommandHandler commandHandler = new CommandHandler();
        private PictureBox canvasShape;
        private List<string> exceptionMessages = new List<string>();
        private readonly VariableManager variableManager = VariableManager.Instance;
        private readonly CommandParser commandParser;

        private bool IsInsideIfBlock = false;
        private bool IfConditionCheck = false;
        private bool IsInsideWhileBlock = false;
        private bool WhileConditionCheck = false;
        private bool IsInsideMethodBlock = false;

        private readonly IfHandler ifCommandHandler;
        private readonly WhileHandler whileCommandHandler;
        private MethodHandler methodHandler;
        private string whileCondition = "";
        List<string> myCommandList = new List<string>();
        List<string> methodCommandList = new List<string>();

        /// <summary>
        /// Initializes a new instance of the DrawHandler class.
        /// </summary>
        /// <param name="canvasWidth">The width of the canvas.</param>
        /// <param name="canvasHeight">The height of the canvas.</param>
        /// <param name="canvasShape">The PictureBox representing the canvas (optional).</param>
        public DrawHandler(int canvasWidth, int canvasHeight, PictureBox canvasShape = null)
        {
            this.canvasShape = canvasShape;
            canvasBitmap = new Bitmap(canvasWidth, canvasHeight);
            commandParser = new CommandParser(this);
            ifCommandHandler = new IfHandler(variableManager);
            whileCommandHandler = new WhileHandler(variableManager);
            methodHandler = new MethodHandler(this, variableManager, exceptionMessages);  // Pass exceptionMessages
        }

        /// <summary>
        /// Executes a single drawing command.
        /// </summary>
        /// <param name="command">The drawing command to execute.</param>
        /// <param name="LineNumber">The line number of the command in a multiline input (optional).</param>
        public void ExecuteCommand(string command, int LineNumber = 0, int totalCommand = 0)
        {
            Console.WriteLine(LineNumber);
            string[] parts = command.Split(' ');
            string commandName = parts[0].ToLower().Trim();
            Console.WriteLine(commandName);
            try
            {
                if (commandName == "if")
                {
                    IsInsideIfBlock = true;
                    // Extract the condition from the command (excluding "if")
                    string condition = command.Substring(2).Trim();

                    // Evaluate the condition
                    bool ifConditionRes = ifCommandHandler.ExecuteIfBlock(condition);

                    // Check if the condition is true
                    if (ifConditionRes)
                    {
                        IfConditionCheck = true;

                    }
                }
                else if (commandName == "endif")
                {
                    // Handle endif statement by resetting IsInsideIfBlock to false
                    IsInsideIfBlock = false;
                    IfConditionCheck = false;
                }
                else if (commandName == "while")
                {
                    IsInsideWhileBlock = true;

                    string condition = command.Substring(5).Trim();
                    whileCondition = condition;

                    // Handle while statement using WhileHandler
                    bool whileConditionRes = whileCommandHandler.HandleWhileLoop(condition);

                    // Check if the condition is true
                    if (whileConditionRes)
                    {
                        WhileConditionCheck = true;
                    }
                }
                else if (IsMethodDefinition(command))
                {
                    IsInsideMethodBlock = true;

                    methodCommandList.Add(command);

                }
                else if (IsMethodCall(command))
                {
                    methodHandler.CallMethod(command);
                }
                else
                {
                    if (IsInsideIfBlock == false && IsInsideWhileBlock == false && IsInsideMethodBlock == false)
                    {
                        HandleShapeDraw(command);
                    }
                    else if (IsInsideIfBlock == true)
                    {
                        if (IfConditionCheck == true)
                        {
                            HandleShapeDraw(command);
                        }

                        Console.WriteLine(LineNumber + " " + totalCommand);
                        if ((LineNumber + 1) == totalCommand)
                        {
                            throw new ArgumentException(new ExceptionHandler().generateException(402, "if", "endif command"));
                        }

                    }
                    else if (IsInsideWhileBlock == true)
                    {

                        if (command.StartsWith("endloop"))
                        {
                            if (WhileConditionCheck == true)
                            {
                                while (whileCommandHandler.HandleWhileLoop(whileCondition) == true)
                                {
                                    foreach (string value in myCommandList)
                                    {
                                        HandleShapeDraw(value);
                                    }
                                }

                                IsInsideWhileBlock = false;
                                WhileConditionCheck = false;
                                whileCondition = "";
                            }
                        }
                        else
                        {
                            if ((LineNumber + 1) == totalCommand)
                            {
                                throw new ArgumentException(new ExceptionHandler().generateException(402, "while", "endloop command"));
                            }
                            myCommandList.Add(command);

                        }
                    }
                
                    else if (IsInsideMethodBlock == true)
                    {
                        if (command.StartsWith("endmethod"))
                        {
                            IsInsideMethodBlock = false;

                            methodHandler.DefineMethod(methodCommandList);
                        }
                        else
                        {
                            if ((LineNumber + 1) == totalCommand)
                            {
                                throw new ArgumentException(new ExceptionHandler().generateException(402, "method", "endmethod command"));
                            }
                            methodCommandList.Add(command);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                exceptionMessages.Add($"Error at line {LineNumber + 1}: {ex.Message}");
            }
        }

        private void HandleShapeDraw(string command)
        {
            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                // Check if the command is a variable assignment
                if (command.Contains("="))
                {
                    HandleVariableAssignment(command);
                }
                else
                {
                    var (shapeName, x, y, width, height, radius, penColorName, fill) = commandParser.ParseCommand(command, canvasBitmap.Width, canvasBitmap.Height);

                    if (shapeName != null)
                    {
                        if (shapeName == "moveto")
                        {
                            commandHandler.MoveTo(x, y);
                        }
                        else if (shapeName == "colour")
                        {
                            SetPenColor(Color.FromName(penColorName));
                        }
                        else if (shapeName == "fill")
                        {
                            fillShapes = fill;
                        }
                        else if (shapeName == "reset")
                        {
                            commandHandler.ResetCursor();
                        }
                        else if (shapeName == "clear")
                        {
                            ClearCanvas();
                        }
                        else
                        {
                            Shape shape = new CreateShape().MakeShape(shapeName);
                            myShapes.Add(shape);
                            shape.Draw(graphics, penColor, commandHandler.CursorPosX, commandHandler.CursorPosY, width, height, radius, fillShapes);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid command or coordinates are out of bounds.");
                    }
                }
                ShowException();

            }
        }

        private void HandleVariableAssignment(string command)
        {
            // Split the command by '=' and remove extra spaces
            string[] assignmentParts = command.Split('=').Select(part => part.Trim()).ToArray();

            if (assignmentParts.Length == 2)
            {
                string variableName = assignmentParts[0];
                string expression = assignmentParts[1];
                    // Check if the variable exists
                    if (variableManager.VariableExists(variableName))
                    {
                        // Evaluate the expression and update the variable value
                        int result = EvaluateExpression(expression);
                        variableManager.SetVariableValue(variableName, result);
                    }
                    else
                    {
                        // If the variable doesn't exist, create it
                        int result = EvaluateExpression(expression);
                        variableManager.SetVariableValue(variableName, result);
                    }
                }
               
//                    throw new ArgumentException(new ExceptionHandler().generateException(402, "variable", "numeric value."));
            else
            {
                throw new ArgumentException(new ExceptionHandler().generateException(402, "variable", "valid variable assignment command."));
            }
        }


        private int EvaluateExpression(string expression)
        {
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            foreach (var variableName in variableManager.GetVariableNames())
            {
                variableValues[variableName] = variableManager.GetVariableValue(variableName);
            }

            return EvaluateExpressionRecursive(expression, variableValues);
        }

        private int EvaluateExpressionRecursive(string expression, Dictionary<string, int> variableValues)
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

        private bool IsMethodCall(string command)
        {
            return command.Contains("(") && command.Contains(")");
        }

        private bool IsMethodDefinition(string command)
        {
            return command.StartsWith("method");
        }

        /// <summary>
        /// Executes a single drawing command.
        /// </summary>
        /// <param name="inputCommand">The drawing command to execute.</param>
        public void ExecuteSingleCommand(string inputCommand)
        {
            //CleanUpCanvas();
            ExecuteCommand(inputCommand);
            ShowException();
        }

        /// <summary>
        /// Executes a multiline drawing command.
        /// </summary>
        /// <param name="inputCommands">The multiline drawing commands to execute.</param>
        public void ExecuteMultilineCommand(string inputCommands)
        {
            ResetVariables();
            CleanUpCanvas();
            string[] commands = inputCommands.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                int totalCommand = commands.Length;
                int lineNumber = 0;
                foreach (string command in commands)
                {
                    // Execute the command for each line
                    ExecuteCommand(command, lineNumber, totalCommand);

                    lineNumber++;
                }

                if (canvasShape != null)
                {
                    canvasShape.Image = (Image)canvasBitmap.Clone();
                }

                ShowException();
            }
        }


        /// <summary>
        /// Resets all variables in the DrawHandler.
        /// </summary>
        public void ResetVariables()
        {
            penColor = Color.Black;
            fillShapes = false;
            myShapes.Clear();
            exceptionMessages.Clear();
            VariableManager.Instance.ClearVariables();
            commandHandler.ResetCursor();
            IsInsideIfBlock = false;
            IfConditionCheck = false;
            IsInsideWhileBlock = false;
            WhileConditionCheck = false;
            IsInsideMethodBlock = false;
            myCommandList.Clear();
            methodCommandList.Clear();
        }

        /// <summary>
        /// Gets the current canvas image.
        /// </summary>
        /// <returns>The current canvas image.</returns>
        public Image GetCanvasImage()
        {
            return (Image)canvasBitmap.Clone();
        }

        /// <summary>
        /// Clears the canvas and exception messages.
        /// </summary>
        public void ClearCanvas()
        {
            exceptionMessages.Clear();
            myShapes.Clear();
            CleanUpCanvas();
        }

        /// <summary>
        /// Clears the canvas bitmap and updates the canvas image.
        /// </summary>
        public void CleanUpCanvas()
        {
            exceptionMessages.Clear();
            canvasBitmap = new Bitmap(canvasBitmap.Width, canvasBitmap.Height);
            UpdateCanvasImage();
        }

        /// <summary>
        /// Displays exception messages on the canvas.
        /// </summary>
        public void ShowException()
        {
            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                using (Font font = new Font("Calibri", 10))
                using (SolidBrush brush = new SolidBrush(Color.Red))
                {
                    int y = 10;
                    foreach (string message in exceptionMessages)
                    {
                        graphics.DrawString(message, font, brush, 10, y);
                        y += 20;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the canvas image on the PictureBox.
        /// </summary>
        private void UpdateCanvasImage()
        {
            canvasShape.Image = (Image)canvasBitmap.Clone();
        }

        /// <summary>
        /// Sets the pen color for drawing shapes.
        /// </summary>
        /// <param name="color">The color to set.</param>
        public void SetPenColor(Color color)
        {
            penColor = color;
        }

        /// <summary>
        /// Gets the current pen color.
        /// </summary>
        /// <returns>The current pen color.</returns>
        public Color GetPenColor()
        {
            return penColor;
        }

        /// <summary>
        /// Sets the pen color for drawing shapes.
        /// </summary>
        /// <param name="penColorName">The name of the color to set.</param>
        internal void SetPenColor(string penColorName)
        {
            throw new ArgumentException();
        }
    }
}
