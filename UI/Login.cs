using BankingApp.Users;
using BankingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.UI
{
    internal class Login
    {
        public static (BasicUser?, bool) Start()
        {
            string prompt = "Login Menu";
            string[] options =
            {
                "Log in",
                "Quit"
            };

            int selectedIndex = Menu.Run(options, prompt);

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    (BasicUser?, bool) user = BasicUser.LogInCheck();
                    Console.Clear();
                    return user;
                case 1:
                    BankApp.Exit();
                    return (null, false);
                default:
                    BankApp.Exit();
                    return (null, false);
            }
        }
    }
}
