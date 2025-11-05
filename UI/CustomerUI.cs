using BankingApp.Utilities;
using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Utilities.Enums;

namespace BankingApp.UI
{
    internal class CustomerUI
    {
        public static void Start(User user)
        {

            string UserPrompt = "Customer Menu ";
            string[] options =
            {
                "Print Action Log",
                "Print Accounts",
                "Request Loan",
                "Open Account",
                "Log out"
            };

            int selectedIndex = Menu.Run(options, UserPrompt);
            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    user.PrintActionLog();
                    Menu.ReturnToStart(user);
                    break;
                case 1:
                    user.PrintAccounts();
                    Menu.ReturnToStart(user);
                    break;
                case 2:
                    Console.WriteLine("Please specify the size of loan which you would like:");
                    decimal loanSize = InputHelpers.ValidDecimal();
                    user.RequestLoan(loanSize, user);
                    Menu.ReturnToStart(user);
                    // Request Loan
                    break;

                case 3:
                    Console.WriteLine("Open Account - Coming Soon");
                    AccountType accountType = CustomerAccount.ChooseAccountType();
                    string currency = CustomerAccount.ChooseCurrency();
                    user.OpenAccount(accountType, currency);
                    Console.WriteLine($"Created {accountType} account with {currency} currency.");
                    Menu.ReturnToStart(user);
                    // Open Account
                    break;
                case 4:
                    Menu.ReturnToLogin();
                    return;

                default:
                    // If error somehow
                    break;
            }
        }
    }
}
