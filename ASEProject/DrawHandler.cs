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
        /// <summary>
        /// The canvas bitmap used for drawing shapes.
        /// </summary>
        private Bitmap canvasBitmap;
        /// <summary>
        /// The current pen color for drawing shapes.
        /// </summary>
        private Color penColor = Color.Black;
        /// <summary>
        /// Flag indicating whether shapes should be filled.
        /// </summary>
        private bool fillShapes = false;
        /// <summary>
        /// List to store the shapes drawn on the canvas.
        /// </summary>
        private List<Shape> myShapes = new List<Shape>();
        /// <summary>
        /// Command handler for processing drawing commands.
        /// </summary>
        private CommandHandler commandHandler = new CommandHandler();
        /// <summary>
        /// PictureBox representing the canvas (optional).
        /// </summary>
        private PictureBox canvasShape;
        /// <summary>
        /// List to store exception messages.
        /// </summary>
        private List<string> exceptionMessages = new List<string>();
        /// <summary>
        /// Instance of VariableManager for managing variables.
        /// </summary>
        private readonly VariableManager variableManager = VariableManager.Instance;
        /// <summary>
        /// Instance of CommandParser for parsing drawing commands.
        /// </summary>
        private readonly CommandParser commandParser;

        private bool IsInsideIfBlock = false;
        private bool IfConditionCheck = false;
        private bool IsInsideWhileBlock = false;
        private bool WhileConditionCheck = false;
        private bool IsInsideNestedWhileBlock = false;
        private bool NestedWhileConditionCheck = false;

        private bool IsInsideMethodBlock = false;

        private readonly IfHandler ifCommandHandler;
        private readonly WhileHandler whileCommandHandler;
        private MethodHandler methodHandler;
        private string whileCondition = "";
        List<string> myCommandList = new List<string>();
        List<string> myCommandList2 = new List<string>();
        List<string> methodCommandList = new List<string>();
        private int loopDepth = 0;
        private int angle = 0;
        private object canvasLock = new object();

        bool whileConditionRes1 = false;

        // Add a new property to store custom shape points
        private List<Point> customShapePoints = new List<Point>();

        // Add a flag to indicate when the parser is inside a custom shape definition
        private bool isInsideCustomShape = false;
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
            methodHandler = new MethodHandler(this, variableManager, exceptionMessages);  
        }

        /// <summary>
        /// Executes a single drawing command.
        /// </summary>
        /// <param name="command">The drawing command to execute.</param>
        /// <param name="LineNumber">The line number of the command in a multiline input (optional).</param>
        public void ExecuteCommand(string command, int LineNumber = 0, int totalCommand = 0)
        {
            lock (canvasLock) // Ensure thread safety
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
                    else if (commandName == "custom")
                    {
                        // Start of a custom shape definition
                        isInsideCustomShape = true;
                        customShapePoints.Clear();
                    }
                    else if (isInsideCustomShape && commandName == "point")
                    {
                        // Add the point to the custom shape
                        var (shapeName, x, y, _, _, _, _, _) = commandParser.ParseCommand(command, canvasBitmap.Width, canvasBitmap.Height);
                        customShapePoints.Add(new Point(x, y));
                    }

                    else if (commandName == "endcustom")
                    {
                        // End of a custom shape definition, draw the shape
                        isInsideCustomShape = false;

                        if (customShapePoints.Count >= 3)
                        {
                            // Assuming a custom shape needs at least 3 points to be valid
                            DrawCustomShape(customShapePoints);
                        }
                        else
                        {
                            throw new ArgumentException("A custom shape must have at least 3 points.");
                        }
                    }
                    else if (commandName == "rotate")
                    {
                        int angleres = commandParser.ParseRotateCommand(command);
                        angle = angleres;
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
                        else if (IsInsideWhileBlock)
                        {
                            if (command.StartsWith("endwhile") && IsInsideNestedWhileBlock == false)
                            {
                                if (WhileConditionCheck)
                                {
                                    while (whileCommandHandler.HandleWhileLoop(whileCondition))
                                    {
                                        foreach (string value in myCommandList)
                                        {
                                            if (value.StartsWith("while"))
                                            {
                                                loopDepth++;
                                                string nestedCondition = value.Substring(5).Trim();

                                                whileCommandHandler.HandleWhileLoop(nestedCondition);

                                                while (whileCommandHandler.HandleWhileLoop(nestedCondition))
                                                {
                                                    IsInsideNestedWhileBlock = true;

                                                    foreach (string nestedValue in myCommandList2)
                                                    {
                                                        HandleShapeDraw(nestedValue);
                                                    }
                                                }

                                                loopDepth--;
                                            }
                                            else
                                            {
                                                if (value.StartsWith("endwhile"))
                                                {
                                                    IsInsideNestedWhileBlock = false;

                                                }
                                                else
                                                {
                                                    HandleShapeDraw(value);
                                                }
                                            }
                                        }
                                    }

                                    IsInsideWhileBlock = false;
                                    WhileConditionCheck = false;
                                    whileCondition = "";
                                }
                            }
                            else
                            {
                                if (command.StartsWith("while"))
                                {
                                    myCommandList2.Add(command);
                                    IsInsideNestedWhileBlock = true;
                                }
                                else if (command.StartsWith("endwhile") && IsInsideNestedWhileBlock == true)
                                {
                                    myCommandList2.Add(command);
                                    IsInsideNestedWhileBlock = false;
                                }

                                myCommandList.Add(command);

                                if ((LineNumber + 1) == totalCommand)
                                {
                                    throw new ArgumentException(new ExceptionHandler().generateException(402, "while", "endwhile comand"));
                                }

                            }
                        }

                        else if (IsInsideMethodBlock == true)
                        {
                            if (command.StartsWith("endmethod"))
                            {
                                IsInsideMethodBlock = false;

                                methodHandler.DefineMethod(methodCommandList);
                                methodCommandList.Clear();

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
        }

        /// <summary>
        /// Handles drawing shapes based on the given command.
        /// </summary>
        /// <param name="command">The drawing command to execute.</param>
        private void HandleShapeDraw(string command)
        {
            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                // Check if the command is a variable assignment
                if (command.Contains("="))
                {
                    variableManager.HandleVariableAssignment(command);
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

                            shape.Draw(graphics, penColor, commandHandler.CursorPosX, commandHandler.CursorPosY, width, height, radius, fillShapes, angle);
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


        /// <summary>
        /// Checks if the given command represents a method call.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>True if the command is a method call, otherwise false.</returns>
        private bool IsMethodCall(string command)
        {
            return command.Contains("(") && command.Contains(")");
        }

        /// <summary>
        /// Checks if the given command represents a method definition.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>True if the command is a method definition, otherwise false.</returns>
        private bool IsMethodDefinition(string command)
        {
            return command.StartsWith("method");
        }

        // Add a method to draw the custom shape
        private void DrawCustomShape(List<Point> points)
        {
            using (Graphics graphics = Graphics.FromImage(canvasBitmap))
            {
                // Assuming a simple polyline for the custom shape
                graphics.DrawPolygon(new Pen(penColor), points.ToArray());
            }
            UpdateCanvasImage();
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
                    lock (canvasLock)
                    {
                        canvasShape.Image = (Image)canvasBitmap.Clone();
                    }
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
            angle = 0;
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
