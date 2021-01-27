using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Net;

namespace TestASP.Services
{
    public class EmailService
    {
        public const string fromLogin = "yourEmail";
        private const string pass = "yourEmailPassword";
        public async Task SendEmailAsync(string emailTo, string subject, string message, string emailFrom = fromLogin, string fromPassword = pass, string smtpHost = "smtp.mail.ru", int smtpPort = 465)
        {
            var mess = new MimeMessage();

            mess.From.Add(new MailboxAddress("FromKek PucName", fromLogin));
            mess.To.Add(new MailboxAddress("", emailTo));
            mess.Subject = subject;
            mess.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using(var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpHost, smtpPort, true);
                await client.AuthenticateAsync(fromLogin, fromPassword);
                await client.SendAsync(mess);

                await client.DisconnectAsync(true);
            }
        }
    }
}
