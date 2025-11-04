using BankingApp.Accounts;
using BankingApp.Utilities;
using BankingApp.Utilities.Enums;
using Org.BouncyCastle.Bcpg;
using System.Net.Mail;
using System.Xml.Linq;

namespace BankingApp.Users
{
    internal class User : BasicUser
    {
        private List<Account> accountList;
        private List<Loan>? loanList;
        public bool IsBlocked { get; set; }
        private decimal Sum;
        public uint CreditScore { get; set; }

        public User(string userName, string name, string phoneNumber, string emailAddress, string password, bool isBlocked, uint creditScore) : base(userName, name, phoneNumber, emailAddress, password)
        {
            accountList = new List<Account>();
            loanList = new List<Loan>();
            IsBlocked = isBlocked;
            CreditScore = creditScore;
        }

        /// <summary>
        /// Prints the action log for all accounts owned by the user
        /// </summary>
        public void PrintActionLog()
        {
            foreach (var a in accountList)
            {
                Console.WriteLine($"Action log for account {a.AccountNumber}:");
                foreach (var log in a.GetLogList())
                {
                    Console.WriteLine(log);
                }
            }
        }

        /// <summary>
        /// Prints all accounts owned by the user
        /// </summary>
        public void PrintAccounts()
        {
            foreach (var a in accountList)
            {
                Console.WriteLine(a);
            }
        }

        /// <summary>
        /// Requests a loan of the specified size for the current user.
        /// </summary>
        /// <remarks>If the loan is approved, it is added to the user's list of loans and a confirmation
        /// message is displayed. If the loan is denied, an error message is displayed indicating the reason for
        /// denial.</remarks>
        /// <param name="loansize">The amount of the loan requested. Must be a positive decimal value.</param>
        public void RequestLoan(decimal loansize, User user)
        {

            if (loansize <= user.Sum * 5)
            {
                Console.WriteLine($"Loan cannot be larger than five times the total sum of your money in the bank. {user.Sum * 5}");
                Console.ReadLine();
            }
            else
            {

                try
                {
                    Loan l = new Loan(this, loansize);
                    Console.WriteLine("Loan was successfully approved and added to your list of loans!:" + l);
                    loanList.Add(l);

                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Loan denied: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Adds a loan to the list of loans.
        /// </summary>
        /// <param name="loan">The loan to be added. Cannot be null.</param>
        public void AddLoan(Loan loan)
        {
            loanList.Add(loan);
        }

        /// <summary>
        /// Removes the specified loan from the loan list.
        /// </summary>
        /// <remarks>This method removes the first occurrence of the specified loan from the list. If the
        /// loan is not found, the list remains unchanged.</remarks>
        /// <param name="loan">The loan to be removed. Must not be null.</param>
        public void RemoveLoan(Loan loan)
        {
            loanList.Remove(loan);
        }

        /// <summary>
        /// Retrieves a collection of all loans.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Loan}"/> containing all loans. The collection will be empty if no loans are
        /// available.</returns>
        /// 
        public List<Loan> GetLoans()
        {
            return new List<Loan>(loanList);
        }
        //public IEnumerable<Loan> GetLoans()
        //{
        //    return loanList.ToList();
        //}


        /// <summary>
        /// Opens a new account of the specified type and currency.
        /// </summary>
        /// <remarks>If <paramref name="accountType"/> is "savings" (case-insensitive), a savings account
        /// is created. Otherwise, a standard account is created. The new account is added to the internal account
        /// list.</remarks>
        /// <param name="accountType">The type of account to open. Valid values are "savings" for a savings account or any other value for a
        /// standard account.</param>
        /// <param name="currency">The currency in which the account will be denominated. For example, "USD" for US dollars or "EUR" for euros.</param>
        public void OpenAccount(AccountType accountType, string currency)
        {

            if (accountType == AccountType.Savings)
            {
                accountList.Add(new SavingsAccount(currency, this));
            }
            else if (accountType == AccountType.Normal)
            {
                accountList.Add(new Account(currency, this));
            }
            else
            {
                Console.WriteLine("Error: Invalid account type specified.");
            }
        }
    }
}