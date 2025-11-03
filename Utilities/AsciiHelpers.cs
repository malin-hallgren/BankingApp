using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal static class AsciiHelpers
    {
        public static string LogoPath { get; private set; } = "../../../Utilities/Logo.txt";

        /// <summary>
        /// Prints everything from a file
        /// </summary>
        /// <param name="path">filepath to print from</param>
        public static void PrintAscii(string path)
        {
            string? toPrint = File.ReadAllText(path);
            Console.WriteLine(toPrint);
        }
    }
}
