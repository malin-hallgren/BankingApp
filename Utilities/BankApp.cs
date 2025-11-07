using BankingApp.Accounts;
using BankingApp.UI;
using BankingApp.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;

namespace BankingApp.Utilities
{
    internal class BankApp
    {
        private static readonly string _filePathUsers = "BasicUserList.json";
        private static List<BasicUser> Users = new List<BasicUser>();
        private static List<Transfer> PendingTransfer = new List<Transfer>();
        public static int Interval { get; private set; } = 1;
        private static System.Timers.Timer _transferTimer = new System.Timers.Timer(Interval * 60000/10);
        private static readonly object _pendingLock = new object();
        private static PasswordHasher<BasicUser> Hasher {  get; set; } = new PasswordHasher<BasicUser>();

        private static readonly string _filePathTransfers = "Transfers.json";
        private static List<Transfer> Transfers = new List<Transfer>();

        private static readonly string _filPathSum = "TransferSum.json";
        private static decimal TransferSum;
        public static bool IsRunning { get; private set; }


        /// <summary>
        /// Sets variables at their start state, loads needed files
        /// </summary>
        public static void Startup()
        {
            IsRunning = true;
            SetTransactionTimer();

            Setup();

            // We need to save a transaction log, and the sum, and boot them here too
            AsciiHelpers.PrintAscii(AsciiHelpers.LogoPath);
            Console.CursorVisible = false;
            Console.WriteLine("Press ENTER to continue to Login...");
            Console.ReadLine();
            Console.Clear();

            while (IsRunning)
            {
                (BasicUser?, bool) loginData = Login.Start();

                if (loginData.Item2)
                {
                    var currentUser = loginData.Item1;

                    if (currentUser != null)
                    {
                        if (currentUser is Admin)
                        {
                            var admin = currentUser as Admin;
                            bool runAdmin = true;

                            do
                            {
                                runAdmin = AdminUI.Start(admin);
                            } while (runAdmin);
                        }
                        else
                        {
                            var user = currentUser as User;
                            bool runUser = true;

                            do
                            {
                                runUser = CustomerUI.Start(user);
                            } while (runUser);
                        }
                    }
                }
            }
            Console.ReadLine();
            Exit();
        }

        /// <summary>
        /// Sets up and loads all the JSON files into correct objects
        /// </summary>
        private static void Setup()
        {
            Users = JsonHelpers.LoadList<BasicUser>(_filePathUsers);
            Transfers = JsonHelpers.LoadList<Transfer>(_filePathTransfers);
            List<Account> allAccounts = new List<Account>();

            foreach (var user in Users)
            {
                if (user.GetType() == typeof(User))
                {
                    var customer = (User)user;
                    foreach (var account in customer.GetAccounts())
                    {
                        account.Owner = customer;
                        allAccounts.Add(account);
                        
                    }
                    foreach (var loan in customer.GetLoans())
                    {
                        loan.Owner = customer;
                    }
                }
            }

            foreach (var transfer in Transfers)
            {
                try
                {
                    transfer.To = allAccounts.Find(x => x.AccountNumber == transfer.ToID);
                    if (transfer.To == null)
                    {
                        throw new InvalidOperationException("To account doesn't exist");
                    }  
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    transfer.From = allAccounts.Find(x => x.AccountNumber == transfer.FromID);
                    if (transfer.From == null)
                    {
                        throw new InvalidOperationException("From account doesn't exist");
                    }    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                if (transfer.To != null && transfer.From != null)
                {
                    transfer.To.AddToLogList(transfer);
                    transfer.From.AddToLogList(transfer);
                }
            }

            if (File.Exists(_filPathSum))
            {
                string json = File.ReadAllText(_filPathSum) ?? new string("0");
                TransferSum = JsonSerializer.Deserialize<decimal>(json);
            }
            else
            {
                TransferSum = 0;
                string json = TransferSum.ToString();
                json = JsonSerializer.Serialize(TransferSum);
                File.WriteAllText(_filPathSum, json);
            }

            if (!Users.Exists(x => x.GetType() == typeof(Admin)))
            {
                Console.WriteLine("No Admin exists, standard admin user generated. Please set a new password below:");
                Admin admin = new Admin("Admin", "admin", "587634876538", "admin@redactedbank.se", "dummy");
                admin.Password = BasicUser.PasswordHash(admin, InputHelpers.ValidString());
                Users.Add(admin);
                JsonHelpers.SaveList(Users, _filePathUsers);

                Console.Clear();
            } 
        }

        /// <summary>
        /// Exits the application, saving neccessary files and prints goodbye message
        /// </summary>
        public static void Exit()
        {
            IsRunning = false;
            Console.Clear();
            JsonHelpers.SaveList(Users, _filePathUsers);

            AsciiHelpers.PrintAscii(AsciiHelpers.LogoPath);
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


        /// <summary>
        /// Gets a copy of the Transfer list
        /// </summary>
        /// <returns>a copy of the PendingTransfers list</returns>
        public static List<Transfer> GetTransferList()
        {
            lock(_pendingLock)
            {
                return new List<Transfer>(PendingTransfer);
            }
        }

        /// <summary>
        /// Prints statistics (transfers and the sum of them all)
        /// </summary>
        public static void PrintStatistics()
        {
            //This is an escape sequence to force clear the entire console, rather than just what is visible
            Console.Clear();
            Console.WriteLine("\x1b[3J");

            foreach (var transfer in Transfers)
            {
                Console.WriteLine(transfer);
                Console.WriteLine(new string('_', Console.BufferWidth));
            }

            Console.WriteLine(new string('_', Console.BufferWidth));
            Console.WriteLine($"Total amount of money transferred in the bank (In SEK): {TransferSum.ToString("F2")}");
        }

        /// <summary>
        /// Adds to the PendingTransfers list
        /// </summary>
        /// <param name="transfer">The transfer to add to the list</param>
        public static void AddToTransferList(Transfer transfer)
        {
            lock (_pendingLock)
            {
                PendingTransfer.Add(transfer);
            }
        }

        /// <summary>
        /// Uses a hardcoded 15 minute timer to run OnTimedEvent, automatically resets upon completion
        /// </summary>
        public static void SetTransactionTimer()
        {
            _transferTimer.Elapsed += SendPendingTransactions;
            _transferTimer.AutoReset = false;
            _transferTimer.Enabled = true;
        }

        /// <summary>
        /// Events to be run upon completion of the Transaction timer, sends and completes transactions in the 
        /// PendingTransactions List
        /// </summary>
        /// <param name="source">The object from which this is sent, in our case, the timer</param>
        /// <param name="time">Data for the ElapsedEvent from the timer, in this case, just a time</param>
        private static void SendPendingTransactions(object? source, ElapsedEventArgs time)
        
        {
            if (PendingTransfer.Count != 0)
            { 
                lock (_pendingLock)
                {

                    foreach (var transfer in PendingTransfer)
                    {

                        transfer.Date = time.SignalTime;
                        transfer.AmountInSEK = transfer.ExecuteTransfer(transfer);

                        
                        TransferSum += transfer.AmountInSEK;
                        string json = TransferSum.ToString();
                        json = JsonSerializer.Serialize(TransferSum);
                        File.WriteAllText(_filPathSum, json);
                        
                        
                        Transfers.Add(transfer);

                        transfer.From.AddToLogList(transfer);
                        transfer.To.AddToLogList(transfer);

                        
                        if(transfer.AmountInSEK >= 20000m)
                        {
                            transfer.SendMail();
                        }
                    }
                    
                    JsonHelpers.SaveList(Transfers, _filePathTransfers);

                    PendingTransfer.Clear();
                }
            }
            _transferTimer.Start();
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
