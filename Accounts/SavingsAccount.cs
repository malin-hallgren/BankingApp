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

        public SavingsAccount(string accountNumber, decimal balance, string currency, User owner, decimal interest)
            : base(accountNumber, balance, currency, owner)
        {
            Interest = interest;
        }

        public override string ToString()
        {
            return $"This savings account has an interest rate of: {Interest}";
        }
    }
}
