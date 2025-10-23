using BankingApp.Accounts;
using System.Net.Mail;
using System.Xml.Linq;

namespace BankingApp.Users
{
    internal class User : BasicUser
    {
        private List<Account>? accountList;
        //private List<Log>? logList;

        // private List<Loan>? loanList;
        public bool IsBlocked { get; set; }

        public uint CreditScore { get; set; }

        User(string userName, string name, string phoneNumber, string emailAddress, string password, bool isAdmin, bool isBlocked, uint creditScore) : base(userName, name, phoneNumber, emailAddress, password, isAdmin)
        {
            accountList = new List<Account>();
            //loanList = new LoanList();
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

        public Account OpenAccount(string guid, decimal balance, string currency, User owner)
        {
            return new Account();
        }
    }
}
