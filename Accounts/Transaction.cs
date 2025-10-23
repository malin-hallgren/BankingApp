using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Accounts
{
    internal class Transaction
    {
        //public uint TransactionID {get; set;}
        //public string TransactionMessage {get; set;}
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Account From { get; set; }
        public Account To { get; set; }

        public Transaction(decimal amount, DateTime date, Account from, Account to)
        {
            Amount = amount;
            Date = date;
            From = from;
            To = to;

        }

        public void SendMail(string message)
        {
            // TODO : Add implementation 
        }

        public override string ToString()
        {
            return $"Amount: {Amount} | Date {Date} | From {From} | To {To}";
        }


    }
}
