﻿namespace EspartoWorld.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;
    using MimeKit.Text;

    public class MailKitEmailSender : IEmailSender
    {
        private readonly string gmailEmail;
        private readonly string password;

        public MailKitEmailSender(string gmailEmail, string password)
        {
            this.gmailEmail = gmailEmail;
            this.password = password;
        }

        public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            using (var smtp = new SmtpClient())
            {
                MimeMessage email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(from));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = htmlContent };
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(this.gmailEmail, this.password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}

