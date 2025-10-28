using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt; // Texten/titeln som displayas
            Options = options; // Menyvalen (array)
            SelectedIndex = 0;
        }

        private void DisplayOptions() // Privat då den kommer köras från "Run"
        {
            Console.WriteLine(Prompt);

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

            // Behövs för att resetta colors efter varje "loop"
            Console.ResetColor();
        }

        public int Run()
        {

            ConsoleKey keyPressed = 0;

            // Körs tills man trycker Enter
            while (keyPressed != ConsoleKey.Enter)
            {
                Console.Clear(); // Rensar konsolen vid varje loop
                DisplayOptions(); // kallar på Display-klassen

                // Läser av tangenten av användaren och sparar den i "keyPressed"
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;

                    // Om man hamnar på index -1 så flyttas man till sista indexen
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }

                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;

                    // Om man hamnar utanför sista menyvalet flyttas man till första indexen
                    if (SelectedIndex > Options.Length - 1)
                    {
                        SelectedIndex = 0;
                    }
                }
            }
            // Skickar tillbaka det index man tryckt enter på (perfekt för att kasta direkt in i en switch)
            return SelectedIndex;
        }
    }
}
