using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal class Menu
    {
        /*
         * HOW TO USE:
         * string prompt = "Header text";
         * string[] options = { "Option 1", "Option 2", "Option 3" };
         * Menu mainMenu = new Menu(prompt, options);
         * 
         * int selectedIndex = mainMenu.Run();
         * 
         * switch (selectedIndex)
         * ..vanlig switch case..
         */

        private int SelectedIndex;
        private int prevIndex;
        private string[] Options;
        private string Prompt;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt; // Header Text
            Options = options; // Menu choices (array of strings)
            SelectedIndex = 0;
            prevIndex = 0;
        }

        private void DisplayOptions() // Private as it will only be called from "Run"
        {
            Console.WriteLine($"{Prompt}\n");

            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];

                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{currentOption}");
            }

            // Reset colors after each "loop"
            Console.ResetColor();
        }

        public int Run()
        {
            Console.CursorVisible = false;

            ConsoleKey keyPressed = 0;

            // Runs until Enter is pressed
            while (keyPressed != ConsoleKey.Enter)
            {
                
                Console.Clear(); // Clears the console each loop
                DisplayOptions(); // Calls the Display method

                // Reads the key pressed by the user and stores it in "keyPressed"
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    prevIndex = SelectedIndex;
                    SelectedIndex--;

                    // If you go under first menu option, move to last index
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                    ClearLines(prevIndex, SelectedIndex);
                }

                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    prevIndex = SelectedIndex;
                    SelectedIndex++;

                    // If you go over last menu option, move to first index
                    if (SelectedIndex > Options.Length - 1)
                    {
                        SelectedIndex = 0;
                    }
                    ClearLines(prevIndex, SelectedIndex);
                }
            }

            Console.CursorVisible = true;
            // Returns the index of the selected option when Enter is pressed (perfect for switch cases)
            return SelectedIndex;
        }

        private static void ClearLines(int prev, int current)
        {
            Console.SetCursorPosition(0, prev + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, current + 2);
            Console.Write(new string(' ', Console.BufferWidth));
        }
    }
}
