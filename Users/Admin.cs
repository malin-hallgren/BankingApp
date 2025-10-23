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
            : base(userName, name, phoneNumber, emailAddress, password, isAdmin: true)
        {
        }

        /// <summary>
        /// Prints a list of all users in the provided list
        /// </summary>
        /// <param name="users">Expects a list of BasicUser objects</param>
        public void ListUsers(List<BasicUser> users)
        {
            foreach (var user in users)
            {
                if (user.GetType() == typeof(User))
                {
                    Console.WriteLine($"User: {user.Name}, Email: {user.EmailAddress}");
                }
            }
        }

        public User CreateUser()
        {

        }
    }
}
