using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal class OutputHelpers
    {
        public static void PrintList<T>(List<T> list)
        {
            if(list.Count < 1)
            {
                Console.WriteLine("Nothing to print");
            }
            else
            {
                foreach (var entry in list)
                {
                    Console.WriteLine($"{entry}\n");
                }
            }
                
        }
    }
}
