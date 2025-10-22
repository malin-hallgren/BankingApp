using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Users
{
    internal class BasicUser
    {
        public static string FilePath { get; private set; } = "BasicUserList.json";
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

        public BasicUser(string name)
        {
            Name = name;
        }
    }
}
