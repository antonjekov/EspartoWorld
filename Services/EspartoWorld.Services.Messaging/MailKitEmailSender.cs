namespace EspartoWorld.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;
    using MimeKit.Text;

    public class MailKitEmailSender : IEmailSender
    {
       public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlContent };

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("espartoworld@gmail.com", "k13antonj");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
