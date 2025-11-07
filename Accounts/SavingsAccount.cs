using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankingApp.Accounts
{
    internal class SavingsAccount : Account
    {
        public decimal Interest { get; set; }
        [JsonInclude]
        public int SavingsPeriod { get; set; }
        private static int[] timePeriod = { 1, 2, 3, 4, 5 };

        //Constructor with no params needed for Json Deserialization
        public SavingsAccount()
        {

        }

        public SavingsAccount(int savingsPeriod, string accountName, string currency, User owner, decimal interest = 3, decimal balance = 0)
            : base(accountName, currency, owner, balance)
        {
            Interest = interest;
            SavingsPeriod = savingsPeriod;
        }

        public void ApplyInterest()
        {
            decimal interestAmount = (Balance * Interest) / 100;
            Balance += interestAmount;  // Uses Account.Balance
        }

        public static decimal SimulateInterestForSavingsAccount(int period)
        {
            if (period < 2)
            {
                return 0.02m;
            }
            else if (period > 1 && period < 3)
            {
                return 0.03m;
            }
            else if (period > 2 && period < 4)
            {
                return 0.04m;
            }
            else
            {
                return 0.5m;
            }          
        }

        public static int[] GetTimePeriodList()
        {
            return (int[])timePeriod.Clone();
        } 

        public override string ToString()
        {
            return $"{base.ToString()}\nThis savings account has an interest rate of: {Interest}%\n";
        }
    }
}
