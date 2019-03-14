using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using web_back_tictactoe.web.Options;

namespace web_back_tictactoe.web.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailTo, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _emailServiceOptions;

        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions)
        {
            _emailServiceOptions = emailServiceOptions.Value;
        }

        public Task SendEmail(string emailTo, string subject, string message)
        {
            try
            {
                using (var client = new SmtpClient(_emailServiceOptions.MailServer, int.Parse(_emailServiceOptions.MailPort)))
                {
                    if (bool.Parse(_emailServiceOptions.UseSSL))
                        client.EnableSsl = true;

                    if (!string.IsNullOrEmpty(_emailServiceOptions.UserId))
                        client.Credentials = new NetworkCredential(_emailServiceOptions.UserId, _emailServiceOptions.Password);

                    client.Send(new MailMessage("lars@k7c.dk", emailTo, subject, message));
                }
            }
            catch (Exception ex)
            {
            }

            return Task.CompletedTask;
        }
    }
}