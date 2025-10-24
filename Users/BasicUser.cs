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
        public static string FilePath { get; private set; } = "BasicUserList.json";
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
    }
}