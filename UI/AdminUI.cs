using BankingApp.Utilities;
using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.UI
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
                "Update Interest Rates",
                "Log out"
            };

            Menu menu = new Menu(prompt, options);
            int selectedIndex = menu.Run();
               
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Admin.CreateUser();
                    Start();
                    break;

                case 1:
                    Console.Clear();
                    List<BasicUser> users = BankApp.GetUserList();
                    OutputHelpers.PrintList(users);
                    Console.ReadLine();
                    Start();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("Update Currencies - Coming soon!");
                    Console.ReadLine();
                    Start();
                    // Update currencies
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("Update Interest Rates - Coming soon!");
                    Console.ReadLine();
                    Start();
                    // Update Interest Rates
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
