
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace AhmadShop.BLL
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("EmailSettings");

            using var smtpClient = new SmtpClient(smtpSettings["Host"])
            {
                Port = Convert.ToInt32(smtpSettings["Port"]),
                Credentials = new NetworkCredential(
                    smtpSettings["Email"],
                    smtpSettings["Password"]),
                EnableSsl = true
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["Email"]!),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
