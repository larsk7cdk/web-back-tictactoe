using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using web_back_tictactoe.web.Options;

namespace web_back_tictactoe.web.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly EmailServiceOptions _emailServiceOptions;
        private readonly ILogger<EmailService> _logger;

        public SendGridEmailService(IOptions<EmailServiceOptions> emailServiceOptions, ILogger<EmailService> logger)
        {
            _emailServiceOptions = emailServiceOptions.Value;
            _logger = logger;
        }

        public Task SendEmail(string emailTo, string subject, string message)
        {
            _logger.LogInformation(
                $"##Start## Sending email via SendGrid to :{emailTo} subject:{subject} message:{message}");
            var client = new SendGridClient(_emailServiceOptions.RemoteServerAPI);
            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress(_emailServiceOptions.UserId)
            };
            sendGridMessage.AddTo(emailTo);
            sendGridMessage.Subject = subject;
            sendGridMessage.HtmlContent = message;
            client.SendEmailAsync(sendGridMessage);

            return Task.CompletedTask;
        }
    }
}