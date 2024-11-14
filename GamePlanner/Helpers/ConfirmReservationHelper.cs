using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.Enums;
using GamePlanner.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Helpers
{
    public class ConfirmReservationHelper(IEmailService emailService)
    {
        private readonly IEmailService _emailService = emailService;
        public string GenerateConfirmationLink(int sessionId, string userId, string token)
        {
            var baseUrl = KeyVaultHelper.GetSecretString("BaseUrl");
            return $"{baseUrl}api/confirm?sessionId={sessionId}&userId={userId}&token={token}";
        }

        public async Task SendConfirmationEmailAsync(string receiver, int sessionId, string userId, string token)
        {
            var uniqueUrl = GenerateConfirmationLink(sessionId, userId, token);
            var templateName = _emailService.GetEmailTemplateName(EmailTemplateEnum.ConfirmReservation);
            var template = await _emailService.GetEmailTemplateAsync(templateName);
            template = _emailService.ComputeEmailTemplate(template, new Dictionary<string, string>
            {
                { "code", uniqueUrl }
            });

            await _emailService.SendEmailAsync(receiver, "Confirm reservation", template);
        }
    }
}
