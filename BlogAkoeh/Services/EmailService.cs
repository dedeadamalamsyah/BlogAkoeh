using BlogAkoeh.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BlogAkoeh.Services
{
    public class EmailService
    {
        private readonly MailSettings _settings;

        public EmailService(IOptions<MailSettings> s)
        {
            _settings = s.Value;
        }

        public async Task<bool> SendAsync(MailData data)
        {
            try
            {
                var mail = new MimeMessage();

                mail.From.Add(new MailboxAddress(
                    _settings.DisplayName,
                    _settings.From
                    ));
                mail.Sender = new MailboxAddress(
                    _settings.DisplayName,
                    _settings.From);

                mail.To.Add(MailboxAddress.Parse(data.To));
                mail.Subject = data.Subject;

                var body = new BodyBuilder();
                body.HtmlBody = data.Message;

                mail.Body = body.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    _settings.Host,
                    _settings.Port,
                    SecureSocketOptions.StartTls
                    );
                await smtp.AuthenticateAsync(
                    _settings.Username,
                    _settings.Password
                    );
                await smtp.SendAsync(mail);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
