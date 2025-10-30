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

            (BasicUser?, bool) loginData = BasicUser.LogInCheck();
            var currentUser = BasicUser.GetUserType(loginData.Item1);

            if (currentUser != null && loginData.Item2 == true)
            {
                //Run menu for appropriate type of user here. User/Admin
            }

            Console.WriteLine($"Login succeeded? {loginData.Item2}. As {currentUser}");

            Console.ReadLine();
            BankApp.Exit();
        }
    }
}
