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
        public string? AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public string? Currency { get; set; }
        public User? Owner { get; set; }

        public Account(string accountNumber, decimal balance, string currency, User owner)
        {
            string AccountNumber = accountNumber;
            Balance = balance;
            Currency = currency;
            Owner = owner;
        }

       public void Transfer(User owner, Account account)
        {
            // TODO: Add implementation 
        }
     



    }
}
