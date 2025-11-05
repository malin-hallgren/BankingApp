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

namespace BankingApp.Accounts
{
    internal class Loan
    {
        public decimal Size { get; set; }
        private  decimal LoanInterest { get; set; }

        [JsonIgnore]
        public User Owner { get; set; }
        public double MortagePercent { get; set; }

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
            if (owner.CreditScore < 100 || size > owner.GetSum() * 5)
            {
                string message = String.Empty;

                if (owner.CreditScore < 100)
                {
                    message += "Loan denied due to low creditscore.\n";
                }
                if (size > owner.GetSum() * 5)
                {
                    message += $"Loan cannot be larger than five times the total sum of your money in the bank.";
                }

                if(String.IsNullOrWhiteSpace(message))
                {
                    message += "Invalid loan";
                }

                throw new InvalidOperationException(message);
            }            

            Size = size;
            Owner = owner;
            CalculateInterest(size);
            owner.AddLoan(this);           
        }
       
        public void AdminUpdateInterest(decimal interest)
        {
            LoanInterest = interest;
        }
        
        //Just a quick draft of calculate interest. We should improve it by adding more factors for the calculation, like income size, repaymeny period etc.

        /// <summary>
        /// Decides interest depending on the size of the Loan
        /// </summary>
        /// <param name="loan"> Size of loan</param>
        private decimal CalculateInterest(decimal loan)
        {
            // Maybe implement an Enum for different loan sizes instead of using hardcoded values
            // Show different interest rates depending on the size of the loan to the user before accepting the loan
            if (loan < 1000000)
            {
                return 0.042m;
            }
            else if (loan > 1000000 && loan < 3000000)
            {
                return 0.039m;
            }
            else if (loan > 3000000 && loan < 5000000)
            {
                return LoanInterest = 0.032m;
            }

            else
            {
                return LoanInterest = 0.025m;
            }
        }

        /// <summary>
        /// Returns a string representation of the loan details, including size, interest, and owner.
        /// </summary>
        /// <returns>A string containing the size of the loan, the interest rate, and the owner's name.</returns>
        public override string ToString()
        {
            return $"Size of loan: {Size} | Interest: {LoanInterest} | Owner: {Owner} ";
        }
    }

}
