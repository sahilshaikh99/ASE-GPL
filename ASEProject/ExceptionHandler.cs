using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    class ExceptionHandler
    {
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
                            return "fsv";
                    }
               }
    }
}