using BankingApp.Accounts;
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

        /*  TODO: Implement
         *  public Loan RequestLoan()
          {
              return;
          }*/

        /// <summary>
        /// Opens a new account of the specified type and currency.
        /// </summary>
        /// <remarks>If <paramref name="accountType"/> is "savings" (case-insensitive), a savings account
        /// is created. Otherwise, a standard account is created. The new account is added to the internal account
        /// list.</remarks>
        /// <param name="accountType">The type of account to open. Valid values are "savings" for a savings account or any other value for a
        /// standard account.</param>
        /// <param name="currency">The currency in which the account will be denominated. For example, "USD" for US dollars or "EUR" for euros.</param>
        public void OpenAccount(string accountType, string currency)
        {

            if (accountType.ToLower().Equals("savings"))
            {
                accountList.Add(new SavingsAccount(currency, this));
            }
            else
            {
                accountList.Add(new Account(currency, this));
            }
        }
    }
}
