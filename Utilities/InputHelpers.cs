using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal class InputHelpers
    {

        public static string ValidString()
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                else
                {
                    Console.WriteLine("Input may not be empty, please make a valid input:");
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                }
            }
        }
    }
}
