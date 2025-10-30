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
        //DONE:
        // skapa en get set för balance och sätt den från konstruktorn 
        // desmma för currency
        
        // TODO:
        // add to action log - när ett account skapas. 


        public decimal Interest { get; set; }
        public decimal Balance { get; set; }


        public SavingsAccount(string currency, User owner, decimal interest = 2, decimal balance = 0)
            : base(currency, owner, balance)
        {
            this.interest = interest;
            this.balance = balance;
            
        }

        // public decimal getInterest(){
        //     return interest;
        // }

        // public decimal setInterest(decimal interest){
        //     this.interest = interest;
        // }


        
        public override string ToString()
        {
           return $"This savings account has an interest rate of: {Interest}";
        }
    }
}
