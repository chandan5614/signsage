using Infrastructure.Interfaces;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public EmailService(string smtpServer, int smtpPort, string senderEmail, string senderPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(recipientEmail);

            using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                smtpClient.EnableSsl = true;

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                    throw new InvalidOperationException("An error occurred while sending the email.", ex);
                }
            }
        }
    }
}
