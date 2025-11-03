using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Utilities;

namespace BankingApp.UI
{
    internal class AccountType
    {
        public static bool Start()
        {
            string prompt = "Which type of Account do you wish to open?";
            string[] options =
            {
                "Normal",
                "Savings"
            };

            int selectedIndex = Menu.Run(options, prompt);

            if (selectedIndex == 0)
            {
                return false;
            }
            else if (selectedIndex == 1)
            {
                return true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something has gone very wrong...");
                return false;
            }
        }
    }
}
