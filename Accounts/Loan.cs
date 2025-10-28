using BankingApp.Users;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Accounts
{
    internal class Loan
    {
        public decimal Size { get; set; }
        public decimal Interest { get; set; }
        public User Owner { get; set; }
        public double MortagePercent { get; set; }

        public Loan(User owner, decimal size, decimal interest)
        {
            Size = size;
            Interest = interest;
            Owner = owner;
            
        }

        public override string ToString()
        {
            return $"Size of loan: {Size} | Interest: {Interest} | Owner: {Owner} ";
        }
    }

}
