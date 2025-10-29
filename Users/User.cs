using BankingApp.Accounts;
using Org.BouncyCastle.Bcpg;
using System.Net.Mail;
using System.Xml.Linq;

namespace BankingApp.Users
{
    internal class User : BasicUser
    {
        private List<Account> accountList;
        //private List<Log>? logList;
        public  List<Loan>? LoanList { get; set; }
        public bool IsBlocked { get; set; }

        public uint CreditScore { get; set; }

        public User(string userName, string name, string phoneNumber, string emailAddress, string password, bool isBlocked, uint creditScore) : base(userName, name, phoneNumber, emailAddress, password)
        {
            accountList = new List<Account>();
            LoanList = new List<Loan>();
            IsBlocked = isBlocked;
            CreditScore = creditScore;
        }

        public void PrintActionLog()
        {

        }

        public void PrintAccounts()
        {
            foreach (var a in accountList)
            {
                // TODO: add implementation when Account Class is done.                
            }

        }

        /*  TODO: Implement
         *  public Loan RequestLoan()
          {
              return;
          }*/

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

            //user.GetType() == typeof(User)
    }
}
