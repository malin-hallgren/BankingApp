using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Users;
using BankingApp.Utilities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankingApp.Accounts
{
    internal class Account
    {
        public string AccountName { get; set; }
        public Guid AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }

        [JsonIgnore]
        public User Owner { get; set; }

        [JsonInclude]
        private List<Transfer> logList;

       
        public Account()
        {
            logList = new List<Transfer>();
        }

        public Account(string accountName, string currency, User owner, decimal balance = 0)
        {
            AccountName = accountName;
            Currency = currency;
            Owner = owner;
            AccountNumber = Guid.NewGuid();
            Balance = balance;
            logList = new List<Transfer>();
        }
        public List<Transfer> GetLogList()
        {
            return new List<Transfer>(logList);
        }

        public void AddToLogList(Transfer toAdd)
        {
            logList.Add(toAdd);
        }

        public static void CreateTransfer(User user)
        {
            Account[] temp = user.GetAccounts().Where(x => x.GetType() != typeof(SavingsAccount)).ToArray();
            string[] userAccounts = Array.ConvertAll(temp, x => x.ToString());

            int selected = Menu.Run(userAccounts, "From which of your accounts do you wish to transfer money?");
            Account from = temp[selected];

            List<Account> allAccounts = new List<Account>();
            foreach (var bu in BankApp.GetUserList())
            {
                if (bu is User)
                {
                    User u = (User)bu;
                    List<Account> uAccounts = u.GetAccounts().Where(x => x.GetType() != typeof(SavingsAccount)).ToList();
                    allAccounts.AddRange(uAccounts);
                }
            }

            temp = allAccounts.ToArray();
            string[] allUserAccounts = Array.ConvertAll(temp, x => x.ToString());

            selected = Menu.Run(allUserAccounts, "To which account do you wish to transfer money?");
            Account to = allAccounts[selected];


            

            Console.Clear();
            
            bool validAmount = false;
            decimal amount = 0; 

            while (!validAmount)
            {
                Console.WriteLine("How much do you wish to transfer?");
                decimal input = InputHelpers.ValidDecimal();

                if (input > from.Balance)
                {
                    Console.WriteLine("Cannot transfer more money than balance of From account.\nPress ENTER to try again...");
                    Console.CursorVisible = false;
                    Console.ReadLine();
                    Console.CursorVisible = true;
                }
                else
                {
                    amount = input;
                    validAmount = true;
                }
            }

            Console.WriteLine("Do you wish to leave a message? Press ENTER when done, note that this may be left blank");
            string? message = Console.ReadLine();

            var transfer = new Transfer(amount, from, to, message);

            Console.WriteLine($"Transfers are sent every {BankApp.Interval} minutes. Your balance will change and the transaction will be displayed in your logs shortly!");
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            logList.Add(new Transfer(amount, this, this, "Deposit"));
        }
        public override string ToString()
        {
            return $"Account name: {accountName}\nBalance: {Balance}\nCurrency: {Currency}\nOwner: {Owner.Name}\nAccount Number: {AccountNumber}";
        }
    }
}
 