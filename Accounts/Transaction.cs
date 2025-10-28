using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Accounts
{
    internal class Transaction
    {
        public Guid TransactionID {get; set;}
        public string TransactionMessage {get; set;}
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Account From { get; set; }
        public Account To { get; set; }

        public Transaction(decimal amount, string message, DateTime date, Account from, Account to)
        {
            Amount = amount;
            TransactionMessage = message;
            Date = date;
            From = from;
            To = to;
            TransactionID = Guid.NewGuid();

        }

        public void SendMail(string mailText)
        {
            // TODO : Add implementation 

            //Added here so that we all can have a look at it. If we want to use this we will need another package
            //MailKit, for some of the methods as the .NET standard one is old, and shitty. Everyone also needs to
            //either set up environment variables targeting my user on SMTP2GO, or set up your own user. This will NOT
            //carry over to anyone downloading it, and would then normally be set server side if this was published... but it works 
            //locally for us like this at least :'). Feel free to delete if we do not want this /MH

            //string? smtpUser = Environment.GetEnvironmentVariable("SMTP2GO_USER");
            //string? smtpPass = Environment.GetEnvironmentVariable("SMTP2GO_PASS");

            //var message = new MimeMessage();

            //try
            //{
            //    message.From.Add(new MailboxAddress("Knock-Off Bank", smtpUser));
            //    message.To.Add(MailboxAddress.Parse(user.Email));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Something went wrong with the email addresses: {ex.Message}");
            //}


            //message.Subject = $"{user.Name} please check your account";
            //message.Body = new TextPart("plain")
            //{
            //    mailText;
            //};

            //using (var client = new SmtpClient())
            //{
            //    try
            //    {
            //        client.Connect("mail.smtp2go.com", 8465, SecureSocketOptions.SslOnConnect);
            //        client.Authenticate(smtpUser, smtpPass);

            //        client.Send(message);
            //        client.Disconnect(true);
            //        Console.WriteLine("Message sent!");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Something went wrong: {ex.Message}");
            //        Console.WriteLine("No message was sent.");

            //        if (smtpUser == null || smtpPass == null)
            //        {
            //            Console.WriteLine("Check your environment variables? Have they been set? Right click project -> Properties -> Debug and set them");
            //            
            //        }
            //    }
            //};
        }

        public override string ToString()
        {
            return $"Amount: {Amount} | Message: {TransactionMessage} | Date {Date} | From {From} | To {To}";
        }


    }
}
