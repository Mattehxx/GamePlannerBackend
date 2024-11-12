using System.Net.Mail;
using System.Net;
using GamePlanner.Services.IServices;
using GamePlanner.DTO.ConfigurationDTO;
using GamePlanner.Helpers;

namespace GamePlanner.Services
{
    public class EmailService : IEmailService
    {
        private SmtpClient? _smtpClient = null;
        private readonly EmailSettingsDTO _emailSettings = KeyVaultHelper.GetSecret<EmailSettingsDTO>("EmailSettings")
            ?? throw new InvalidOperationException(nameof(_emailSettings));

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _smtpClient ??= new SmtpClient(_emailSettings.SMPTServer, _emailSettings.SMPTPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.EmailAddress, _emailSettings.EmailPassword)
            };

            return _smtpClient.SendMailAsync(
                new MailMessage(
                    from: _emailSettings.EmailAddress,
                    to: email,
                    subject,
                    message
                )
            );
        }

        public Task SendEmailAsync(IEnumerable<string> emails, string subject, string message)
        {
            _smtpClient ??= new SmtpClient(_emailSettings.SMPTServer, _emailSettings.SMPTPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.EmailAddress, _emailSettings.EmailPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.EmailAddress),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            foreach (var email in emails)
            {
                mailMessage.To.Add(email);
            }

            return _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
