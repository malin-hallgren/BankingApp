using BankingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Menus
{
    internal class AdminUI
    {
        // This is called with AdminUI.Start
        public static void Start()
        {
            string prompt = "Admin Menu"; 
            string[] options =
            {
                "Create new user",
                "Display all users",
                "Update currencies",
                "Update Interest Rates"
            };

            Menu menu = new Menu(prompt, options);
            int selectedIndex = menu.Run();
               
            switch (selectedIndex)
            {
                case 0:
                    // Create new user
                    break;

                case 1:
                    // Display all users
                    break;

                case 2:
                    // Update currencies
                    break;

                case 3:
                    // Update Interest Rates
                    break;

                default:
                    // If error somehow
                    break;
            }
        }
    }
}
