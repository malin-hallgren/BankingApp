using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Accounts
{
    internal class SavingsAccount : Account
    {
        public decimal Interest { get; set; }

        public SavingsAccount(string currency, User owner, decimal interest = 3, decimal balance = 0)
            : base(currency, owner, balance)
        {
            Interest = interest;
        }

        public void ApplyInterest()
        {
            decimal interestAmount = (Balance * Interest) / 100;
            Balance += interestAmount;  // Använder Account.Balance
        }

        public override string ToString()
        {
            return $"{base.ToString()} | This savings account has an interest rate of: {Interest}%";
        }
    }
}
