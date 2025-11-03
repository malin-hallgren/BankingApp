using BankingApp.Users;
using BankingApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal class Menu
    {
        public static int SelectedIndex = 0;

        private static void DisplayOptions(string[] options, string title) // Private as it will only be called from "Run"
        {
            Console.WriteLine($"{title}\n");

            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];

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

        public static int Run(string[] options, string title)
        {
            Console.CursorVisible = false;

            ConsoleKey keyPressed = 0;

            int prevIndex = SelectedIndex;

            // Runs until Enter is pressed
            while (keyPressed != ConsoleKey.Enter)
            {
                
                Console.Clear(); // Clears the console each loop
                DisplayOptions(options, title); // Calls the Display method

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
                        SelectedIndex = options.Length - 1;
                    }
                    ClearLines(prevIndex, SelectedIndex);
                }

                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    prevIndex = SelectedIndex;
                    SelectedIndex++;

                    // If you go over last menu option, move to first index
                    if (SelectedIndex > options.Length - 1)
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

        //Specific Menus below

        

        public static void ReturnToStart(BasicUser user)
        {
            if (user.GetType() == typeof(User))
            {
                Console.ReadLine();
                User Customer = (User)user;
                CustomerUI.Start(Customer);
            }

            else
            {
                Console.ReadLine();
                Admin admin = (Admin)user;
                AdminUI.Start(admin);
            }
        }

        public static void ReturnToLogin()
        {
            Console.WriteLine("Logged out successfully. Please log in again if you wish to continue.\nPress ENTER to continue...");
        }
    }
}
