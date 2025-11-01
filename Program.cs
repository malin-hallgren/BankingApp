using BankingApp.Users;
using BankingApp.Utilities;
using BankingApp.Accounts;
using BankingApp.UI;

namespace BankingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            BankApp.Startup();

            while (BankApp.IsRunning)
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
                            AdminUI.Start();
                        }
                        else
                        {
                            var user = currentUser as User;
                            CustomerUI.Start(user);
                        }
                    }
                }
                
            }
            

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
