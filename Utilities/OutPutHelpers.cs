using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal class OutputHelpers
    {
        /// <summary>
        /// Prints the inputted list
        /// </summary>
        /// <typeparam name="T">Type contained in list, specify if needed</typeparam>
        /// <param name="list">List to print</param>
        public static void PrintList<T>(List<T> list)
        {
            if(list.Count < 1)
            {
                Console.WriteLine("Nothing to print");
            }
            else
            {
                foreach (var entry in list)
                {
                    Console.WriteLine($"{entry}\n");
                }
            }     
        }

        /// <summary>
        /// Prints a string in the selected color on the same line
        /// </summary>
        /// <param name="toHighlight">the string to highlight</param>
        /// <param name="color">the color to print in</param>
        public static void Highlight(string toHighlight, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(toHighlight);
            Console.ResetColor();
        }


        /// <summary>
        /// Prints part of a string in the specified color, on the same line
        /// </summary>
        /// <param name="first">the first, uncolored part of the string</param>
        /// <param name="toHighlight">the colored part</param>
        /// <param name="second">the trailing uncolored part of the string</param>
        /// <param name="color">the color in which to highlight</param>
        public static void HighlightFragment(string first, string toHighlight, string second, ConsoleColor color)
        {
            Console.Write(first);
            Highlight(toHighlight, color);
            Console.Write(second);
        }
    }
}
