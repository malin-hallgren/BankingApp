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
                "Deposit Money",
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
                    AccountType accountType = ChooseAccountType();
                    string currency = ChooseCurrency();
                    Console.WriteLine("Enter account name:");
                    string accountName = InputHelpers.ValidString();
                    if (accountType == AccountType.Savings)
                    {
                        user.OpenAccount( accountName, accountType, currency, ChooseSavingsPeriod());
                    }
                    else
                    {
                        user.OpenAccount(accountName, accountType, currency);
                    }
                    Console.WriteLine($"\n{accountType} account created with {currency} currency.");
                    Menu.ReturnToStart();
                    return true;
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
            OutputHelpers.Highlight("Deposit succeeded!", ConsoleColor.DarkGreen);
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
                OutputHelpers.Highlight("Loan request cancelled.", ConsoleColor.DarkRed);
            }
        }

        public static AccountType ChooseAccountType()
        {
            string prompt = "Which type of Account do you wish to open?";
            string[] options =
            {
                "Normal",
                "Savings"                
            };

            int selectedIndex = Menu.Run(options, prompt);

            if (selectedIndex == 0)
            {
                Console.WriteLine("Creating normal account");
                return AccountType.Normal;
            }
            else if (selectedIndex == 1)
            {
                Console.WriteLine("Creating savings account");
                return AccountType.Savings;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something has gone very wrong...");
                return AccountType.Error;
            }
        }

        public static string ChooseCurrency()
        {
            int numberOfKeys = ConvertCurrencies.GetDictionary().Count;
            string prompt = "Which currency do you wish to use for your account?";
            string[] options = ConvertCurrencies.GetDictionary().Keys.ToArray();
            int selectedIndex = Menu.Run(options, prompt);
            return options[selectedIndex];
        }

        private static int ChooseSavingsPeriod()
        {
            string prompt = "Choose between the different timeperiods OBS! your money will not be accessible during this time";
            int[] options = SavingsAccount.GetTimePeriodList();
            string[] strings = Array.ConvertAll(options, x => x.ToString());
            int selectedIndex = Menu.Run(strings, prompt);
            return options[selectedIndex];          
        } 
    }
}

