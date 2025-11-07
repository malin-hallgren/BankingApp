using BankingApp.Users;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using BankingApp.Utilities;

namespace BankingApp.Accounts
{
    internal class Loan
    {
        public decimal Size { get; set; }
        
        [JsonInclude]
        private decimal LoanInclInterest { get; set; }
        [JsonIgnore]
        public User Owner { get; set; }
        public decimal EffectiveInterest { get; set; } //this is local for an individual loan

        //Constructor with no params needed for Json Deserialization
        public Loan()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Loan"/> class with the specified owner and loan size.
        /// </summary>
        /// <remarks>This constructor calculates the interest based on the loan size and associates the
        /// loan with the owner.</remarks>
        /// <param name="owner">The user who owns the loan. Must have a credit score of 100 or higher.</param>
        /// <param name="size">The size of the loan to be created.</param>
        /// <exception cref="InvalidOperationException">Thrown if the owner's credit score is less than 100.</exception>
        public Loan(User owner, decimal size)
        {
            if (size > owner.GetSum() * 5)
            {
                string message = String.Empty;

                if (size > owner.GetSum() * 5)
                {
                    message += $"Loan cannot be larger than five times the total sum of your money in the bank.";
                }

                if (String.IsNullOrWhiteSpace(message))
                {
                    message += "Invalid loan";
                }

                throw new InvalidOperationException(message);
            }

            Size = size;
            Owner = owner;
            EffectiveInterest = CalculateInterest(size);
            LoanInclInterest = Size + Size * EffectiveInterest;
        }

        /// <summary>
        /// Decides interest depending on the size of the Loan
        /// </summary>
        /// <param name="loanSize"> Size of loan</param>
        private static decimal CalculateInterest(decimal loanSize)
        {
            decimal baseRate = BankApp.GetBaseRateLoan();

            // Maybe implement an Enum for different loan sizes instead of using hardcoded values
            // Show different interest rates depending on the size of the loan to the user before accepting the loan
            if (loanSize < 1000000)
            {
                return baseRate;
            }
            else if (loanSize > 1000000 && loanSize < 3000000)
            {
                return baseRate - 0.0025m;
            }
            else if (loanSize > 3000000 && loanSize < 5000000)
            {
                return baseRate - 0.0032m;
            }
            else
            {
                return baseRate - 0.0039m;
            }
        }

        public static decimal SimulateInterest(decimal size)
        {
            decimal simulatedInterest = CalculateInterest(size);
            return simulatedInterest * 100;
        }

        /// <summary>
        /// Returns a string representation of the loan details, including size, interest, and owner.
        /// </summary>
        /// <returns>A string containing the size of the loan, the interest rate, and the owner's name.</returns>
        public override string ToString()
        {
            return $"Loan size: {Size} \nInterest rate: {(EffectiveInterest * 100).ToString("F2")}\nLoan incl. interest rate: {((EffectiveInterest + 1) * Size).ToString("F")}\n\nBorrower: \n{Owner}";
        }
    }

}
