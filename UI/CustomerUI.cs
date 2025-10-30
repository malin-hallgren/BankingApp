using BankingApp.Utilities;
using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.UI
{
    internal class CustomerUI
    {
        // This is called with CustomerUI.Start
        public static void Start(User user)
        {
            string prompt = "Customer Menu " + user.Name; // Might be cool if this íncludes "user.name" or something. It absolutely can, long as we send the User object along!
            string[] options =
            {
                "Print Action Log",
                "Print Accounts",
                "Request Loan",
                "Open Account",
                "Log out"
            };

            Menu menu = new Menu(prompt, options);
            int selectedIndex = menu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    //OutputHelpers.PrintList(user.logList);
                    Console.WriteLine("Action List - Coming Soon");
                    Console.ReadLine();
                    Start(user);
                    // Print Action Log
                    break;

                case 1:
                    Console.Clear();
                    user.PrintAccounts();
                    Console.WriteLine("Account List - Coming Soon");
                    Console.ReadLine();
                    Start(user);
                    // Print Accounts
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("Request Loan - Coming Soon");
                    Console.ReadLine();
                    Start(user);
                    // Request Loan
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("Open Account - Coming Soon");
                    Console.ReadLine();
                    Start(user);
                    // Open Account
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Logged out successfully. Please log in again");
                    return;

                default:
                    // If error somehow
                    break;
            }
        }
    }
}
