using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public const string ApplicationName = "Social Network";
        public const string ApplicationEmail = "lavishstory@gmail.com";
        public const string ApplicationEmailPassword = "Lavishstory123";
        public const string GmailSmtp = "smtp.gmail.com";
        public const int Port = 587;

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient(GmailSmtp, Port)) // netstat -l/a ... for checking available ports
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ApplicationEmail, ApplicationEmailPassword);

                var mailMessage = new MailMessage();
                mailMessage.To.Add(email);
                mailMessage.From = new MailAddress(ApplicationEmail, ApplicationName);
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = false;

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}