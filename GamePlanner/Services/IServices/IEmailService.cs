using GamePlanner.Enums;

namespace GamePlanner.Services.IServices
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
        public Task SendEmailAsync(IEnumerable<string> emails, string subject, string message);
        public Task SendConfirmationEmailAsync(string receiver, string name, int sessionId, string userId, string token);
        public Task SendDeleteEmailAsync(string receiver, string name, int reservationId);    
        public Task SendQueuedEmailAsync(string receiver, string name, string eventName);
        public string GetEmailTemplateName(EmailTemplateEnum template);
        public Task<string> GetEmailTemplateAsync(string templateName);
        public string ComputeEmailTemplate(string template, Dictionary<string, string> values);
        public string GenerateConfirmationLink(int sessionId, string userId, string token);
        public string GenerateDeleteLink(int reservationId);
    }
}
