using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using BankingApp.Users;
using BankingApp.Accounts;

namespace BankingApp
{
    internal class BankApp
    {
        private static List<BasicUser> Users = new List<BasicUser>();
        private static List<Transaction> PendingTransactions = new List<Transaction>();
        private static System.Timers.Timer _transactionTimer = new System.Timers.Timer(15 * 60000);
        public static decimal TransactionSum;

        /// <summary>
        /// Retrieves a list of all basic users.
        /// </summary>
        /// <returns>Returns a copy of the list <see cref="BasicUser"/> to prevent the external code from modifying the entire list
        /// present.</returns>
        public static List<BasicUser> GetUserList()
        {
            return new List<BasicUser>(Users);
        }

        /// <summary>
        /// Adds a user to the Users list
        /// </summary>
        /// <param name="user"></param>
        public static void AddUserToList(BasicUser user)
        {
            Users.Add(user);
        }

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

            //TODO_MH: Make sure this isn't run in creating the Transaction
            //Deducts and adds funds in from and to accouts, sets correct time for transaction
            //and adds amount of transaction to the total, TransactionSum
            //foreach (var transaction in PendingTransactions)
            //{
            //    Console.WriteLine(transaction);
            //    transaction.From.Balance -= transaction.Amount;
            //    transaction.To.Balance += transaction.Amount;
            //    transaction.Date = time.SignalTime;
            //    TransactionSum += transaction.Amount;
            //}

            string message = $"The following transactions have been carried out at {time.SignalTime}";
            foreach (var transaction in PendingTransactions)
            {
                Console.WriteLine(transaction);
            } //How do we want to display this? Thinking a log for the admin, select the date, and upon selecting the time a list of the transactions?

            PendingTransactions.Clear();
        }
    }
}
