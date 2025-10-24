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
        public Admin(string userName, string name, string phoneNumber, string emailAddress, string password)
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

        public void CreateUser(string userName, string name, string phoneNumber, string emailAddress, string password, uint creditScore)
        {
            User newUser = new User(userName, name, phoneNumber, emailAddress, password, isBlocked: false, creditScore);
        }
    }
}
