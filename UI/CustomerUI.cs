using BankingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Menus
{
    internal class CustomerUI
    {
        // This is called with CustomerUI.Start
        public static void Start()
        {
            string prompt = "Customer Menu"; // Might be cool if this íncludes "user.name" or something
            string[] options =
            {
                "Print Action Log",
                "Print Accounts",
                "Request Loan",
                "Open Account"
            };

            Menu menu = new Menu(prompt, options);
            int selectedIndex = menu.Run();

            switch (selectedIndex)
            {
                case 0:
                    // Print Action Log
                    break;

                case 1:
                    // Print Accounts
                    break;

                case 2:
                    // Request Loan
                    break;

                case 3:
                    // Open Account
                    break;

                default:
                    // If error somehow
                    break;
            }
        }
    }
}
