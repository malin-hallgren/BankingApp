using BankingApp.Utilities;
using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using BankingApp.Accounts;

namespace BankingApp.UI
{
    internal class AdminUI
    {
        // This is called with AdminUI.Start
        public static bool Start(Admin admin)
        {
            
            string prompt = "Admin Menu"; 
            string[] options =
            {
                "Create new user",
                "Display all users",
                "Update currencies",
                "Update Interest Rates",
                "Unblock User",
                "Show Bank Data",
                "Log out"
            };

            int selectedIndex = Menu.Run(options, prompt);
            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    Admin.CreateUser();
                    Menu.ReturnToStart();
                    return true;
                case 1:
                    List<BasicUser> users = BankApp.GetUserList();
                    OutputHelpers.PrintList(users);
                    Menu.ReturnToStart();
                    return true;
                case 2:
                    UpdateCurrencyUI();
                    Console.Clear();
                    Console.WriteLine("New Exchannge Rates:");
                    ConvertCurrencies.DrawFull();
                    Console.WriteLine("\nPress Enter to return to menu");
                    Menu.ReturnToStart();
                    return true;
                case 3:
                    Console.WriteLine("Input new interest");
                    var input = InputHelpers.ValidDecimal()/100;

                    Admin.UpdateInterest(input);
                    Menu.ReturnToStart();
                    return true;
                case 4:
                    User[] blockedUsers = BankApp.GetListOfBlockedUsers();
                    if (blockedUsers.Length == 0)
                    {
                        Console.WriteLine("No blocked users found.");
                        Menu.ReturnToStart();
                        return true;
                    }
                    string userNameToUnblock = UnblockUser();
                    if (!string.IsNullOrEmpty(userNameToUnblock))
                    {
                        User userToUnblock = blockedUsers.FirstOrDefault(u => u.Name == userNameToUnblock);
                        if (userToUnblock != null)
                        {
                            userToUnblock.IsBlocked = false;
                            // Update the user in the list
                            BankApp.AddToUserList(userToUnblock);
                            Console.WriteLine($"User {userToUnblock.Name} has been unblocked.");
                        }
                    }
                    Menu.ReturnToStart();
                    return true;
                case 5:
                    BankApp.PrintStatistics();
                    Menu.ReturnToStart();
                    return true;
                case 6:
                    Menu.ReturnToLogin();
                    return false;
                default:
                    return true;
            }
        }

        public static void UpdateCurrencyUI()
        {
            string prompt = "Choose currency to edit from the list";
            string[] option = ConvertCurrencies.GetDictionary().Keys.ToArray();
            
            int selectedIndex = Menu.Run(option, prompt);

            Console.Write($"Set new value for {option[selectedIndex]}: ");
            string currency = option[selectedIndex].ToString();
            decimal newRate = InputHelpers.ValidDecimal();

            ConvertCurrencies.setRate(currency, newRate);
        }

        public static string UnblockUser()
        {
            string prompt = "Which user do you wish to unblock?";
            User[] blockedUsers = BankApp.GetListOfBlockedUsers();

            // Create an array with usernames for the menu
            string[] options = blockedUsers.Select(u => u.Name).ToArray();

            if (options.Length == 0)
            {
                Console.WriteLine("No blocked users found.");
                return string.Empty;
            }

            int selectedIndex = Menu.Run(options, prompt);
            return options[selectedIndex];
        }
    }
}