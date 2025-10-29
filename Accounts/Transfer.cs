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

        public Transfer(decimal amount, DateTime date, Account from, Account to, string message = "")
        {
            Amount = amount;
            TransactionMessage = message;
            Date = date;
            From = from;
            To = to;
            TransactionID = Guid.NewGuid();
            BankApp.AddToTransferList(this);
        }

        public void SendMail()
        {
            // TODO : Add implementation 

            //Added here so that we all can have a look at it. If we want to use this we will need another package
            //MailKit, for some of the methods as the .NET standard one is old, and shitty. Everyone also needs to
            //either set up environment variables targeting my user on SMTP2GO, or set up your own user. This will NOT
            //carry over to anyone downloading it, and would then normally be set server side if this was published... but it works 
            //locally for us like this at least :'). Feel free to delete if we do not want this /MH

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
