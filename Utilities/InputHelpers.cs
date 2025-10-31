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

        public static string InputPassword()
        {
            List<char> chars = new List<char>();
            string input = "";
            int index = 0;

            ConsoleKeyInfo keyPressed;

            while (true)
            {
                keyPressed = Console.ReadKey(intercept: false);

                var consolePos = Console.GetCursorPosition();

                if (keyPressed.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyPressed.Key == ConsoleKey.Backspace)
                {
                    if (chars.Count > 0)
                    {
                        chars.RemoveAt(chars.Count - 1);
                        Console.Write(' ');
                        Console.Write("\b");

                    }

                }
                else if (chars.Count >= 1)
                {
                    Console.SetCursorPosition(chars.Count - 1, consolePos.Item2);
                    Console.Write("*");
                    Console.SetCursorPosition(consolePos.Item1, consolePos.Item2);
                    chars.Add(keyPressed.KeyChar);
                }
                else
                {
                    chars.Add(keyPressed.KeyChar);
                }
            }


            input = new string(chars.ToArray());

            if (String.IsNullOrWhiteSpace(input))
            {
                Console.Write("You have not entered a password, alternatively entered only whitespace. Press ENTER to try again...");
                Console.ReadLine();
                Console.Clear();
            }
            return input.ToLower();
        }
    }
}
