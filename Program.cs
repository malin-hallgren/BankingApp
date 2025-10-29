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
        }
    }
}
