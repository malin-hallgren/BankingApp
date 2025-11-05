using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Utilities;
using BankingApp.Utilities.Enums;
namespace BankingApp.UI
{
    internal class CustomerAccount
    {
        public static AccountType
         ChooseAccountType()
        {
            string prompt = "Which type of Account do you wish to open?";
            string[] options =
            {
                "Normal",
                "Savings"
            };

            int selectedIndex = Menu.Run(options, prompt);

            if (selectedIndex == 0)
            {
                Console.WriteLine("Creating normal account");
                return AccountType.Normal;
            }
            else if (selectedIndex == 1)
            {
                
                Console.WriteLine("Creating savings account");
                return AccountType.Savings;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something has gone very wrong...");
                return AccountType.Error;
            }


        }

        public static string ChooseCurrency()
        {
            string prompt = "Which currency do you wish to use for your account?";
            string[] options =
            {
                "SEK",
                "USD",
                "EUR",
                "NOK",
                "DKK",
                "AUD",
                "CAD"
            };

            int selectedIndex = Menu.Run(options, prompt);

            return options[selectedIndex];



        }

    }
}
