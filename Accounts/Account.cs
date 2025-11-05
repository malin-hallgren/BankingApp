using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Users;

namespace BankingApp.Accounts
{
    internal class Account
    {
        public Guid AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public User Owner { get; set; }

        private List<Transfer> logList;



        public Account(string currency, User owner, decimal balance = 0)
        {
            AccountNumber = Guid.NewGuid();
            Balance = balance;
            Currency = currency;
            Owner = owner;
            logList = new List<Transfer>();
        }
        public List<Transfer> GetLogList()
        {
            return new List<Transfer>(logList);
        }

        public void CreateTransfer(decimal amount, DateTime date, Account from, Account to)
        {
            logList.Add(new Transfer(amount, date, from, to));
        }

        public override string ToString()
        {
            return $"Account Number: {AccountNumber}\nBalance: {Balance}\nCurrency: {Currency}\nOwner: {Owner.Name}\n";
        }
    }
}
 