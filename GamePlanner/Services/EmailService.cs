using System.Net.Mail;
using System.Net;
using GamePlanner.Services.IServices;
using GamePlanner.DTO.ConfigurationDTO;
using GamePlanner.Helpers;
using GamePlanner.Enums;

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

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.EmailAddress),
                To = { email },
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            return _smtpClient.SendMailAsync(mailMessage);
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

        public async Task SendConfirmationEmailAsync(string receiver, int sessionId, string userId, string token)
        {
            var uniqueUrl = GenerateConfirmationLink(sessionId, userId, token);
            var templateName = GetEmailTemplateName(EmailTemplateEnum.ConfirmReservation);
            var template = await GetEmailTemplateAsync(templateName);
            template = ComputeEmailTemplate(
                template,
                new Dictionary<string, string>{
                    { "code", uniqueUrl }
                }
            );

            await SendEmailAsync(receiver, "Confirm reservation", template);
        }

        public string GetEmailTemplateName(EmailTemplateEnum template)
        {
            return template switch
            {
                EmailTemplateEnum.ConfirmReservation => "ConfirmReservationEmailTemplate",
                _ => throw new ArgumentOutOfRangeException(nameof(template), template, null),
            };
        }

        public async Task<string> GetEmailTemplateAsync(string templateName)
        {
            if (!File.Exists($"Templates/{templateName}.html"))
            {
                throw new FileNotFoundException($"Template {templateName} not found");
            }
            return string.Concat(await File.ReadAllLinesAsync($"Templates/{templateName}.html"));
        }

        public string ComputeEmailTemplate(string template, Dictionary<string, string> values)
        {
            foreach (var (key, value) in values)
            {
                template = template.Replace("{{" + key + "}}", value);
            }

            return template;
        }

        public string GenerateConfirmationLink(int sessionId, string userId, string token)
        {
            var baseUrl = KeyVaultHelper.GetSecretString("BaseUrl");
            return $"{baseUrl}api/confirm?sessionId={sessionId}&userId={userId}&token={token}";
        }
    }
}
