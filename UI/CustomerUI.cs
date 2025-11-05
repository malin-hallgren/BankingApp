using BankingApp.Accounts;
using BankingApp.Users;
using BankingApp.Utilities;
using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Utilities.Enums;
using System.ComponentModel;

namespace BankingApp.UI
{
    internal class CustomerUI
    {
        public static bool Start(User user)
        {

            string UserPrompt = "Customer Menu ";
            string[] options =
            {
                "Print Action Log",
                "Print Accounts",
                "Request Loan",
                "Open Account",
                "Deposit",
                "Log out"
                };

            int selectedIndex = Menu.Run(options, UserPrompt);
            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    user.PrintActionLog();
                    Menu.ReturnToStart();
                    return true;
                case 1:
                    user.PrintAccounts();
                    Menu.ReturnToStart();
                    return true;
                case 2:
                    Console.WriteLine("Please specify the size of loan which you would like:");
                    decimal loanSize = InputHelpers.ValidDecimal();
                    user.RequestLoan(loanSize, user);
                    Menu.ReturnToStart();
                    return true;

                case 3:
                    AccountType accountType = CustomerAccount.ChooseAccountType();
                    string currency = CustomerAccount.ChooseCurrency();
                    Console.WriteLine("Enter account name:");
                    string accName = InputHelpers.ValidString();
                    user.OpenAccount(accName, accountType, currency);
                    Console.WriteLine($"Created {accountType} account with {currency} currency.");
                    Menu.ReturnToStart();
                    return true;
                case 4:
                    DepositAmount(user);
                    Menu.ReturnToStart();
                    return true;

                case 5:
                    Menu.ReturnToLogin();
                    return false;

                default:
                    // If error somehow
                    return true;
            }
        }

        public static void DepositAmount(User user)
        {
            Console.Clear();
            Account[] temp = user.GetAccounts().ToArray();
            string[] stringArr = Array.ConvertAll(temp, x => x.AccountName);
            int selectedIndex = Menu.Run(stringArr, "Which account do you wish to make a deposit to?");
            Account acc = user.GetAccounts()[selectedIndex];
            Console.WriteLine("Enter deposit amount:");
            acc.Deposit(InputHelpers.ValidDecimal());
        }
    }
}

