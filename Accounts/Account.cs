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
        public Guid AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }

        [JsonIgnore]
        public User Owner { get; set; }

        private List<Transfer> logList;



        public Account(string currency, User owner, decimal balance = 0)
        {
            AccountNumber = Guid.NewGuid();
            Balance = balance;
            Currency = currency;
            Owner = owner;
            logList = new List<Transfer>();
        }
        public List<Transfer> GetLogList()
        {
            return new List<Transfer>(logList);
        }

        public void CreateTransfer(User user)
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

            Account to = allAccounts[selected];

            temp = allAccounts.ToArray();
            string[] allUserAccounts = Array.ConvertAll(temp, x => x.ToString());

            selected = Menu.Run(allUserAccounts, "To which account do you wish to transfer money?");
            

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

            logList.Add(transfer);
            BankApp.AddToTransferList(transfer);
        }

        public override string ToString()
        {
            return $"Account Number: {AccountNumber}, Balance: {Balance}, Currency: {Currency}, Owner: {Owner.Name}";
        }
    }
}
 