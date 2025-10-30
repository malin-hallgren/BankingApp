using BankingApp.Users;
using BankingApp.Utilities;
using BankingApp.Accounts;

namespace BankingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            BankApp.Startup();
            Console.ReadLine();
            
            BankApp.Exit();

            // User user1 = new User("JD", "John", "03555445", "johndoe@gmail.com", "Doepasssword", false, 152);
            // SavingsAccount savingsAccount1 = new SavingsAccount("USD", user1, 3.5m);
            // decimal balance = savingsAccount1.getBalance();
            // Console.WriteLine("balance: " + balance);
            // savingsAccount.setBalance(10000000);
            // Console.WriteLine("balance: " + balance);
        }
    }
}
