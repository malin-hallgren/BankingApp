using BankingApp.Utilities;
using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

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
                    Console.WriteLine("Update Currencies - Coming soon!");
                    Menu.ReturnToStart();
                    return true;
                case 3:
                    Console.WriteLine("Input new interest");
                    var input = InputHelpers.ValidDecimal();
                    admin.UpdateInterestForAllLoans(BankApp.GetUserList(), input);
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
    }
}