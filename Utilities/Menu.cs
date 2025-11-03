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
        private static void DisplayOptions(string[] options, string title, int selected) // Private as it will only be called from "Run"
        {
            Console.WriteLine($"{title}\n");

            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];

                if (i == selected)
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

            int selectedIndex = 0;
            int prevIndex = selectedIndex;

            // Runs until Enter is pressed
            while (keyPressed != ConsoleKey.Enter)
            {
                
                Console.Clear(); // Clears the console each loop
                DisplayOptions(options, title, selectedIndex); // Calls the Display method

                // Reads the key pressed by the user and stores it in "keyPressed"
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    prevIndex = selectedIndex;
                    selectedIndex--;

                    // If you go under first menu option, move to last index
                    if (selectedIndex == -1)
                    {
                        selectedIndex = options.Length - 1;
                    }
                    ClearLines(prevIndex, selectedIndex);
                }

                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    prevIndex = selectedIndex;
                    selectedIndex++;

                    // If you go over last menu option, move to first index
                    if (selectedIndex > options.Length - 1)
                    {
                        selectedIndex = 0;
                    }
                    ClearLines(prevIndex, selectedIndex);
                }
            }

            Console.CursorVisible = true;
            // Returns the index of the selected option when Enter is pressed (perfect for switch cases)
            return selectedIndex;
        }

        private static void ClearLines(int prev, int current)
        {
            Console.SetCursorPosition(0, prev + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, current + 2);
            Console.Write(new string(' ', Console.BufferWidth));
        }


        public static void ReturnToStart(BasicUser user)
        {
            Console.CursorVisible = false;
            Console.WriteLine("\nPress ENTER to return to menu...");
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
            Console.WriteLine("Logged out successfully. Please log in again if you wish to continue.\n\nPress ENTER to continue...");
        }
    }
}
