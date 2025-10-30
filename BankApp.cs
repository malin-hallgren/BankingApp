using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Timers;
using BankingApp.Users;
using BankingApp.Accounts;
using BankingApp.Utilities;
using System.Runtime.CompilerServices;

namespace BankingApp
{
    internal class BankApp
    {
        private static readonly string _filePathUsers = "BasicUserList.json";
        private static List<BasicUser> Users = new List<BasicUser>();
        private static List<Transfer> PendingTransfer = new List<Transfer>();
        private static System.Timers.Timer _transferTimer = new System.Timers.Timer(15 * 60000);
        private static PasswordHasher<BasicUser> Hasher {  get; set; } = new PasswordHasher<BasicUser>();
        public static decimal TransferSum;


        /// <summary>
        /// Sets variables at their start state, loads needed files
        /// </summary>
        public static void Startup()
        {
            Users = JsonHelpers.LoadList<BasicUser>(_filePathUsers);
            // We need to save a transaction log, and the sum, and boot them here too
            AsciiHelpers.PrintAscii(AsciiHelpers.LogoPath);
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Exits the application, saving neccessary files and prints goodbye message
        /// </summary>
        public static void Exit()
        {
            Console.Clear();
            JsonHelpers.SaveList(Users, _filePathUsers);
            Console.WriteLine("Thank you for using *REDACTED* Bank! We look forward to your next visit!");
            Environment.Exit(0);
        }

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
        public static void AddToUserList(BasicUser user)
        {
            Users.Add(user);
            JsonHelpers.SaveList(Users, _filePathUsers);
        }

        public static List<Transfer> GetTransferList()
        {
            return new List<Transfer>(PendingTransfer);
        }

        public static void AddToTransferList(Transfer transfer)
        {
            PendingTransfer.Add(transfer);
        }

        /// <summary>
        /// Uses a hardcoded 15 minute timer to run OnTimedEvent, automatically resets upon completion
        /// </summary>
        public static void SetTransactionTimer()
        {
            _transferTimer.Elapsed += SendPendingTransactions;
            _transferTimer.AutoReset = true;
            _transferTimer.Enabled = true;
        }

        /// <summary>
        /// Events to be run upon completion of the Transaction timer, sends and completes transactions in the 
        /// PendingTransactions List
        /// </summary>
        /// <param name="source">The object from which this is sent, in our case, the timer</param>
        /// <param name="time">Data for the ElapsedEvent from the timer, in this case, just a time</param>
        private static void SendPendingTransactions(Object? source, ElapsedEventArgs time)
        {
            foreach (var transfer in PendingTransfer)
                {
                    Console.WriteLine(transfer);
                    transfer.ExecuteTransfer();
                    transfer.Date = time.SignalTime;
                    TransferSum += transfer.Amount;
                }

            string message = $"The following transactions have been carried out at {time.SignalTime}";
            foreach (var transfer in PendingTransfer)
            {
                Console.WriteLine(transfer);
            } //How do we want to display this? Thinking a log for the admin, select the date, and upon selecting the time a list of the transactions?

            PendingTransfer.Clear();
        }

        /// <summary>
        /// Hashes the password entry for a user
        /// </summary>
        /// <param name="user">the user for which the password is valid for</param>
        /// <param name="plainText">password in plain text</param>
        /// <returns></returns>
        public static string PasswordHash(BasicUser user, string plainText)
        {
            return Hasher.HashPassword(user, plainText);
        }

        /// <summary>
        /// Logs in a user unless it is blocked
        /// </summary>
        /// <returns>Currently returns a bool, potentially should return the user?</returns>
        public static bool LogInCheck()
        {
            bool ongoingLogin = true;
            int attempts = 0;
            var loginStatus = new PasswordVerificationResult();
            string blockedMessage = $"The account has been blocked due to repeated failed access attempts. Please contact an admin";

            while (ongoingLogin)
            {
                Console.WriteLine("Input your username:");

                string username = InputHelpers.ValidString().ToLower();
                if (Users.Exists(x => x.UserName.Contains(username)))
                {
                    var user = Users.Find(x => x.UserName.Contains(username));
                    Console.Clear();

                    //Break out to method CheckBlock?
                    if (user is User)
                    {
                        User current = (User)user;
                        if(current.IsBlocked)
                        {
                            Console.WriteLine(blockedMessage);
                            break;
                        }
                    }

                    while(ongoingLogin && attempts < 3)
                    {
                        Console.WriteLine("Input your password");
                        string input = InputHelpers.ValidString();

                        attempts++;

                        loginStatus = Hasher.VerifyHashedPassword(user, user.Password, input);

                        if(loginStatus == PasswordVerificationResult.Failed)
                        {
                            Console.Clear();
                            Console.WriteLine($"Incorrect password entered. You have {3 - attempts} attempts left.");
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Welcome to *REDACTED* Bank, {user.Name}");
                            Console.ReadLine();
                            ongoingLogin = false;
                            break;
                        }
                    }
                    if (attempts >= 3 && user is User)
                    {
                        Console.Clear();

                        User currentUser = (User)user;
                        currentUser.IsBlocked = true;
                        Console.WriteLine(blockedMessage);
                    }
                    break;
                }
                else
                {
                    
                    Console.Clear();
                    Console.WriteLine("No User with that username exists.\nPress ENTER to try again");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
            }
            return loginStatus == PasswordVerificationResult.Success;
        }

        [Obsolete("Asked more senior programmer, this is more hassle than it is worth. Use specific getters GetUserList, GetTransferList")]
        /// <summary>
        /// Alternative Get List method that allows for use with both Users and Pending Transactions
        /// </summary>
        /// <typeparam name="T">The type of the List you wish to access. Currently supports BasicUser and Transfer</typeparam>
        /// <returns>A copy of the list corresponding to the passed type</returns>
        public static List<T> GetList<T>()
        {
            if (typeof(T).IsAssignableFrom(typeof(BasicUser)))
            {
                var toReturn = new List<BasicUser>(Users);
                return new List<T>((List<T>)(object)toReturn);
            }

            else if (typeof(T).IsAssignableFrom(typeof(Transfer)))
            {
                var toReturn = new List<Transfer>(PendingTransfer);
                return new List<T>((List<T>)(object)toReturn);
            }
            else
            {
                Console.WriteLine("No applicable list found, returning empty, generic list");
                return new List<T>();
            }
        }

        [Obsolete("Asked more senior programmer, this is more hassle than it is worth. Use specific setters AddToUserList and AddToTransfer List")]
        /// <summary>
        /// Alternative Add method usable for both PendingTransactions and Users list (and any others we may add)
        /// </summary>
        /// <typeparam name="T">The type of object you wish to add, currently supports BasicUser(and subclasses) and Transfer</typeparam>
        /// <param name="toAdd">The specific object of the aforementioned types you wish to add</param>
        public static void AddToList<T>(T toAdd)
        {
            if (toAdd is BasicUser)
            {
                if (toAdd is User)
                {
                    var user = toAdd as User;
                    Users.Add(user);
                }
                else
                {
                    var admin = toAdd as Admin;
                    Users.Add(admin);
                }

                JsonHelpers.SaveList(Users, _filePathUsers);
                Console.WriteLine($"{toAdd} was added to the Users list");
            }
            else if (toAdd is Transfer)
            {
                var transfer = toAdd as Transfer;
                PendingTransfer.Add(transfer);
                Console.WriteLine($"{toAdd} was added to the Pending Transactions list");
            }
        }
    }
}
