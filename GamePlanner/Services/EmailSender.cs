
using System.Net.Mail;
using System.Net;

namespace GamePlanner.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smptServer;
        private readonly int _smptPort;
        private readonly string _emailAddress;
        private readonly string _password;

        public EmailSender(IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            _smptServer = emailSettings.GetValue<string>("Server") ?? throw new InvalidOperationException("Missing Server setting");
            _smptPort = emailSettings.GetValue<int?>("Port") ?? throw new InvalidOperationException("Missing Port setting");
            _emailAddress = emailSettings.GetValue<string>("Address") ?? throw new InvalidOperationException("Missing Address setting");
            _password = emailSettings.GetValue<string>("Password") ?? throw new InvalidOperationException("Missing Password setting");
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_smptServer, _smptPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailAddress, _password)
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: _emailAddress,
                    to: email,
                    subject,
                    message
                )
            );
        }
    }
}
