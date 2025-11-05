using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using BankingApp.Utilities;

namespace BankingApp.Accounts
{
    internal class Transfer
    {
        public Guid TransactionID { get; set; }
        public string TransactionMessage { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountInSEK { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public Account From { get; set; }
        
        public Guid FromID {  get; set; }
        [JsonIgnore]
        public Account To { get; set; }
        
        public Guid ToID { get; set; }

        //This is for JSON
        public Transfer()
        {
            TransactionID = Guid.NewGuid();
        }

        public Transfer(decimal amount, Account from, Account to, string? message = "")
        {
            Amount = amount;
            TransactionMessage = message;
            From = from;
            FromID = From.AccountNumber;
            To = to;
            ToID = To.AccountNumber;
            TransactionID = Guid.NewGuid();
            BankApp.AddToPendingTransferList(this);
        }


        /// <summary>
        /// Sends an email to the owner of the account which the transfer is made from
        /// </summary>
        public void SendMail()
        {
            //Note: Aldor, when testing this, you need this file or the mail won't send. Reach out if you want to verify function.
            string[] auth = File.ReadAllLines("../../../Utilities/Environment.env");

            string? smtpUser = auth[0];
            string? smtpPass = auth[1];

            var message = new MimeMessage();

            try
            {
                message.From.Add(new MailboxAddress("*REDACTED* Bank", smtpUser));
                message.To.Add(MailboxAddress.Parse(From.Owner.EmailAddress));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong with the email addresses: {ex.Message}");
            }


            message.Subject = $"{From.Owner.Name} please check your account";
            message.Body = new TextPart("plain")
            {
               Text = $"Dear {From.Owner.Name},\nWe at *REDACTED* Bank care greatly for your security, and your finances. With this in mind we wish to inform you that there has been unusual activity on one of your accounts, or rather, a large transaction. We advice you to check your accounts, and reach out to us immediately, should something seem amiss.\n\nBest Regards,\nThe *REDACTED* Team"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("mail.smtp2go.com", 8465, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(smtpUser, smtpPass);

                    client.Send(message);
                    client.Disconnect(true);
                    Console.WriteLine("Message sent!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong: {ex.Message}");
                    Console.WriteLine("No message was sent.");

                    if (smtpUser == null || smtpPass == null)
                    {
                        Console.WriteLine("Check your environment file, is it complete?");

                    }
                }
            }
        }

        /// <summary>
        /// Deducts and adds specified sum to correct accounts
        /// </summary>
        /// <returns>the amount in SEK</returns>
        public decimal ExecuteTransfer(Transfer transfer)
        {
            From.Balance -= Amount;
            (decimal, decimal) converted = ConvertCurrencies.Convert(transfer.Amount, transfer.From.Currency, transfer.To.Currency);
            To.Balance += converted.Item1;

            return converted.Item2;
        }

        public override string ToString()
        {
            if (From == To)
            {
                return $"Deposit amount: {Amount}";
            }
            return $"Date {Date}\nAmount: {Amount}\nMessage: {TransactionMessage}\nFrom: {From}\nTo: {To}";
        }
    }
}
