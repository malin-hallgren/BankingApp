using BankingApp.Accounts;

namespace BankingApp.Users
{
    internal class User : BasicUser
    {
        private List<Account>? accountList;
        //private List<Log>? logList;
        public bool IsBlocked { get; set; }

        public uint CreditScore { get; set; }

        User(bool isBlocked, uint creditScore)
        {
            accountList = new List<Account>();
            IsBlocked = isBlocked;
            CreditScore = creditScore;
        }

        public void voidActionLog()
        {

        }

        public void PrintAccounts()
        {
            foreach (var a in accountList)
            {
                // TODO: add implementation when Account Class is done.                
            }

        }
    }
}
