using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal class ConvertCurrencies
    {
        // List of supported currencies
        private static Dictionary<string, decimal> _exchangeRates = new()
        {
            {"SEK",1.000m},
            {"USD",0.110m},
            {"EUR",0.092m},
            {"NOK",1.070m},
            {"DKK",0.680m},
            {"AUD",0.160m},
            {"CAD",0.150m},

        };


        // Convert currency from -> to
        public static (decimal, decimal) Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            // Transform input to upper characters (if user writes usd instead of USD)   
            fromCurrency = fromCurrency.ToUpper();
            toCurrency = toCurrency.ToUpper();

            // Catches non supported currencies
            if (!_exchangeRates.ContainsKey(fromCurrency) || !_exchangeRates.ContainsKey(toCurrency))
            {
                Console.WriteLine("That is not a supported currency yet");
            }

            // Converting "from currency" to sek and multiplies the result by the "to currency" exchange rate. 
            decimal amountInSEK = amount / _exchangeRates[fromCurrency];
            decimal convertedAmount = amountInSEK * _exchangeRates[toCurrency];

            // Rounds to x amount of decimals
            return (Math.Round(convertedAmount, 2), amountInSEK);
        }

        // Set new exchangerate for a input currency (currency to change, new rate) (for Admin)
        public static void setRate(string currency, decimal newRate)
        {
            // Transform input to upper characters (if user writes usd instead of USD)   
            currency = currency.ToUpper();

            // Sets the Exchange rate for chosen currency to "newRate"
            if (_exchangeRates.ContainsKey(currency))
            {
                _exchangeRates[currency] = newRate;
            }

            // Catches bad input by user
            else
            {
                Console.WriteLine($"That is not a supported currency yet");
            }
        }
    }
}
