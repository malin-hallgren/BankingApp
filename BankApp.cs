using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using BankingApp.Users;

namespace BankingApp
{
    internal class BankApp
    {
        public List<BasicUser> Users;
        private List<Transaction> PendingTransactions;
        private static System.Timers.Timer _transactionTimer = new System.Timers.Timer(15 * 60000);
        public static decimal TransactionSum;


        /// <summary>
        /// Uses a hardcoded 15 minute timer to run OnTimedEvent, automatically resets upon completion
        /// </summary>
        public static void SetTransactionTimer()
        {
            _transactionTimer.Elapsed += SendPendingTransactions;
            _transactionTimer.AutoReset = true;
            _transactionTimer.Enabled = true;
        }

        /// <summary>
        /// Events to be run upon completion of the Transaction timer, sends and completes transactions in the 
        /// PendingTransactions List
        /// </summary>
        /// <param name="source">The object from which this is sent, in our case, the timer</param>
        /// <param name="time">Data for the ElapsedEvent from the timer, in this case, just a time</param>
        private static void SendPendingTransactions(Object? source, ElapsedEventArgs time)
        {
            Console.WriteLine($"Pending transactions have been carried out at {time.SignalTime}");
            foreach (var transaction in PendingTransactions)
            {
                Console.WriteLine(transaction);
                transaction.From.Balance -= transaction.Amount;
                transaction.To.Balance += transaction.Amount;
                transaction.Date = time.SignalTime;
                TransactionSum += transaction.Amount;
            }
        }
    }
}
