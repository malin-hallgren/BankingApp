using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Users
{
    // should we make this class abstract?
    internal class BasicUser
    {
        // Properties
        
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        // Constructor
        public BasicUser(string userName, string name, string phoneNumber, string emailAddress, string password)
        {
            UserName = userName;
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Password = password;
        }

        public override string ToString()
        {
            return $"Name: {Name}\nUsername: {UserName}\nPhone Number: {PhoneNumber}\nEmail Address: {EmailAddress}";
        }

        public static string GenerateUsername(User user)
        {
            string[] parts = user.Name.Trim().Split(' ');
            string userName = " ";
            
            foreach (var name in parts)
            {
                userName += name.ToLower().Substring(0, 2);
            }

            userName = userName.Trim();

            List<BasicUser> currentUsers = BankApp.GetUserList();
            List<BasicUser> matchingUsers = currentUsers.Where(x => x.UserName.StartsWith(userName)).ToList();


            int indexer = matchingUsers.Count + 1;
            userName = userName + indexer.ToString();

            return userName;
        }
    }
}