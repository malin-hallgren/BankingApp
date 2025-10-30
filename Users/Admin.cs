using BankingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Users
{
    internal class Admin : BasicUser
    {

        // Constructor
        public Admin(string name, string userName, string phoneNumber, string emailAddress, string password)
            : base(userName, name, phoneNumber, emailAddress, password)
        {
        }

        /// <summary>
        /// Prints a list of all users in the provided list
        /// </summary>
        /// <param name="users">Expects a list of BasicUser objects</param>
        public void ListUsers()
        {
            List<BasicUser> users = BankApp.GetUserList();
            foreach (var user in users)
            {
                if (user.GetType() == typeof(User))
                {
                    Console.WriteLine($"User: {user.Name}, Email: {user.EmailAddress}");
                }
            }
        }

        public static  void CreateUser()
        {
            List<string> fields = new List<string>() {"Name:", "Username:", "Phone number:", "Email address:", "Password:" };
            User newUser = new User("", "", "", "", "", false, 0);


            foreach (var field in fields)
            {
                Console.WriteLine(field);
            }

            Console.SetCursorPosition(20, 0);
            newUser.Name = InputHelpers.ValidString();

            newUser.UserName = GenerateUsername(newUser);
            Console.SetCursorPosition(20, 1);
            Console.WriteLine(newUser.UserName);

            Console.SetCursorPosition(20, 2);
            newUser.PhoneNumber = InputHelpers.ValidString();

            Console.SetCursorPosition(20, 3);
            newUser.EmailAddress = InputHelpers.ValidString();

            Console.SetCursorPosition(20, 4);

            newUser.Password = BasicUser.PasswordHash(newUser, InputHelpers.ValidString());

            BankApp.AddToUserList(newUser);


            Console.Clear();

      
        }

    }
}
