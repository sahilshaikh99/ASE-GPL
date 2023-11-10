using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    /// <summary>
    /// This class handles the generation of exception messages based on exception codes.
    /// </summary>
    class ExceptionHandler
    {
        /// <summary>
        /// Generates an exception message based on the provided exception code, type, and additional message.
        /// </summary>
        /// <param name="exceptionCode">The code representing the type of exception.</param>
        /// <param name="exceptionType">The type of exception, e.g., command type.</param>
        /// <param name="exceptionMessage">Additional information about the exception.</param>
        /// <returns>The generated exception message.</returns>
        public string generateException(int exceptionCode, string exceptionType, string exceptionMessage)
        {
            switch (exceptionCode)
            {
                case 401:
                    return "Please enter command.";
                case 402:
                    return "Invalid " + exceptionType + " command: Requires " + exceptionMessage + ". ";
                case 403:
                    return "Invalid " + exceptionType + " command: X and Y Coordinates are out of bounds.";
                default:
                    return "Oops something went wrong!";
            }
        }
    }
}
