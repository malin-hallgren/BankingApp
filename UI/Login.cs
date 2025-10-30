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

            Menu menu = new Menu(prompt, options);
            int selectedIndex = menu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    (BasicUser?, bool) user = BasicUser.LogInCheck();
                    Console.Clear();
                    return user;
                case 1:
                    Console.Clear();
                    BankApp.Exit();
                    return (null, false);
                default:
                    Console.Clear();
                    BankApp.Exit();
                    return (null, false);
            }
        }
    }
}
