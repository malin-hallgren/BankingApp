using BankingApp.Users;
using BankingApp.Utilities;

namespace BankingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AsciiHelpers.PrintAscii(AsciiHelpers.LogoPath);

            Admin adminUser = new Admin("admin1", "Admin User", "111-222-3333", "admin@example.com", "adminpassword");
            //User newUser1 = new User("gmedina", "Geovanni Medina", "123-456-7890", "geovanni@example.com", "password123", false,700);
            //User newUser2 = new User("AndresRTM", "Andres Llano", "987-654-3210", "andres@example.com", "password456", false, 750);

            adminUser.CreateUser("gmedina", "Geovanni Medina", "123-456-7890", "geovannii@example.com", "password123", 700);
            adminUser.CreateUser("AndresRTM", "Andres Llano", "987-654-3210", "andres@example.com", "password456", 750);
            //BankApp.AddUserToList();

            foreach (var user in BankApp.GetUserList())
            {
                Console.WriteLine($"User: {user.Name}, Email: {user.EmailAddress}");
            }


        }
    }
}
