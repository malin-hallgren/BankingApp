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
                    BankApp.PrintStatistics();
                    Menu.ReturnToStart();
                    return true;
                case 5:
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
    }
}