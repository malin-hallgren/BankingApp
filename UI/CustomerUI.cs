using BankingApp.Accounts;
using BankingApp.Users;
using BankingApp.Utilities;
using BankingApp.Utilities.Enums;
using System;
using System.Linq;

namespace BankingApp.UI
{
    internal class CustomerUI
    {
        public static bool Start(User user)
        {

            string UserPrompt = "Customer Menu ";
            string[] options =
            {
                "Transfer Money",
                "Print Action Log",
                "Print Accounts",
                "Print Loans",
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
                    Account.CreateTransfer(user);
                    Menu.ReturnToStart();
                    return true;
                case 1:
                    user.PrintActionLog();
                    Menu.ReturnToStart();
                    return true;
                case 2:
                    user.PrintAccounts();
                    Menu.ReturnToStart();
                    return true;
                case 3:
                    user.PrintLoans();
                    Menu.ReturnToStart();
                    return true;
                case 4:
                    Console.WriteLine("Please specify the size of loan which you would like:");
                    decimal loanSize = InputHelpers.ValidDecimal();
                    UserRequestLoan(user, loanSize);
                    Menu.ReturnToStart();
                    return true;
                case 5:
                    AccountType accountType = CustomerAccount.ChooseAccountType();
                    string currency = CustomerAccount.ChooseCurrency();
                    Console.WriteLine("Enter account name:");
                    string accName = InputHelpers.ValidString();
                    user.OpenAccount(accName, accountType, currency);
                    Console.WriteLine($"Created {accountType} account with {currency} currency.");
                    Menu.ReturnToStart();
                    return true;
                //case 4:
                //      AccountType accountType = CustomerAccount.ChooseAccountType();
                //      string currency = CustomerAccount.ChooseCurrency();
                //      user.OpenAccount(accountType, currency);
                //      Console.WriteLine($"\n{accountType} account created with {currency} currency.");
                //      Menu.ReturnToStart();
                //      return true;
                case 6:
                    DepositAmount(user);
                    Menu.ReturnToStart();
                    return true;
                case 7:
                    Menu.ReturnToLogin();
                    return false;
                default:
                    // If error somehow
                    return true;
            }
        }

        /// <summary>
        /// Prompts the user to select an account and deposit a specified amount into it.
        /// </summary>
        /// <remarks>This method displays a menu for the user to select an account, prompts for a deposit
        /// amount,  and updates the selected account's balance. The deposit amount must be a valid decimal
        /// value.</remarks>
        /// <param name="user">The user whose accounts are available for deposit. Cannot be null.</param>
        private static void DepositAmount(User user)
        {
            Console.Clear();
            Account[] temp = user.GetAccounts().ToArray();
            string[] stringArr = Array.ConvertAll(temp, x => x.AccountName);
            int selectedIndex = Menu.Run(stringArr, "Which account do you wish to make a deposit to?");
            Account acc = user.GetAccounts()[selectedIndex];
            Console.Clear();
            Console.WriteLine("Enter deposit amount:");
            acc.Deposit(InputHelpers.ValidDecimal());
            Console.WriteLine("Deposit succeeded!");
        }

        /// <summary>
        /// Prompts the user to confirm a loan request and processes the request if confirmed.
        /// </summary>
        /// <remarks>This method displays a confirmation prompt to the user, showing the loan amount, the
        /// calculated interest rate,  and the total amount including interest. If the user confirms, the loan request
        /// is processed. If the user cancels,  the operation is aborted.</remarks>
        /// <param name="u">The user requesting the loan. Cannot be <see langword="null"/>.</param>
        /// <param name="l">The loan amount requested. Must be a positive value.</param>
        private static void UserRequestLoan(User u, decimal l)
        {
            Console.Clear();
            string[] prompt = { "Yes", "No" };
            int selectedIndex = Menu.Run(prompt, $"You requested a loan of {l} and the interest of that loan request results in a interest rate of {Loan.SimulateInterest(l).ToString("F2")}%." +
                $"\nTotal loan amount incl. interest: {(l * ((Loan.SimulateInterest(l) / 100)+1)).ToString("F2")}.\nWould you want to proceed with the loan request?");
            Console.Clear();
            if (selectedIndex == 0)
            {
                try
                {
                    u.RequestLoan(l, u);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Loan request failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Loan request cancelled.");
            }
        }
    }
}

