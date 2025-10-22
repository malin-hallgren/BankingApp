using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Transactions;

namespace BankingApp
{
    internal class BankApp
    {
        //public List<BasicUser> Users;
        //private List<Transaction> PendingTransactions;
        private static System.Timers.Timer _transactionTimer = new System.Timers.Timer(1 * 6000);
        public static decimal TransactionSum;

        public static void SetTimer()
        {
            _transactionTimer.Elapsed += OnTimedEvent;
            _transactionTimer.AutoReset = true;
            _transactionTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object? source, ElapsedEventArgs time)
        {
            SendPendingTransactions(time);
        }


        /// <summary>
        /// Carries out pending transactions
        /// </summary>
        /// <param name="time">ElapsedEventsArgs from the OnTimedEven, contains a time and nothing more</param>
        private static void SendPendingTransactions(ElapsedEventArgs time)
        {
            Console.WriteLine($"Pending transactions have been carried out at {time.SignalTime}");
            //foreach(var transaction in PendingTransactions)
            //{
            //    Console.WriteLine(transaction);
            //}

        }

        public static void AddTransaction(Transaction transaction)
        {
            //TransactionSum += transaction.Amount;
        }
    }
}
