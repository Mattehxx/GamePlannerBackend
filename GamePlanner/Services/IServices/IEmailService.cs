using GamePlanner.Enums;

namespace GamePlanner.Services.IServices
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
        public Task SendEmailAsync(IEnumerable<string> emails, string subject, string message);
        public string GetEmailTemplateName(EmailTemplateEnum template);
        public Task<string> GetEmailTemplate(string templateName);
        public string ComputeEmailTemplate(string template, Dictionary<string, string> values);
    }
}
