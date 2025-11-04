using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using BankingApp.Utilities;

namespace BankingApp.Accounts
{
    internal class Transfer
    {
        public Guid TransactionID {get; set;}
        public string TransactionMessage {get; set;}
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Account From { get; set; }
        public Account To { get; set; }

        public Transfer(decimal amount, Account from, Account to, string? message = "")
        {
            Amount = amount;
            TransactionMessage = message;
            From = from;
            To = to;
            TransactionID = Guid.NewGuid();
            BankApp.AddToTransferList(this);
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
                message.To.Add(MailboxAddress.Parse(To.Owner.EmailAddress));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong with the email addresses: {ex.Message}");
            }


            message.Subject = $"{To.Owner.Name} please check your account";
            message.Body = new TextPart("plain")
            {
               Text = "Lol hej"
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
            ;
        }

        /// <summary>
        /// Deducts and adds specified sum to correct accounts
        /// </summary>
        public void ExecuteTransfer()
        {
            To.Balance += Amount;
            From.Balance -= Amount;
        }
         
        public override string ToString()
        {
            return $"Amount: {Amount} | Message: {TransactionMessage} | Date {Date} | From {From} | To {To}";
        }


    }
}
