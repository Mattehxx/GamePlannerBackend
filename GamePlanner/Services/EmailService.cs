using System.Net.Mail;
using System.Net;
using GamePlanner.Services.IServices;
using GamePlanner.DTO.ConfigurationDTO;

namespace GamePlanner.Services
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        private readonly EmailSettingsDTO _emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettingsDTO>() 
            ?? throw new InvalidOperationException();

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_emailSettings.SMPTServer, _emailSettings.SMPTPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.EmailAddress, _emailSettings.EmailPassword)
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: _emailSettings.EmailAddress,
                    to: email,
                    subject,
                    message
                )
            );
        }
    }
}
