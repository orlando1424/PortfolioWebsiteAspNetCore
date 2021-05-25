using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;

namespace PortfolioWebsiteAspNetCore.Models
{
    public class MailKitEEmailService : IEmailService
    {
        private readonly EmailServerConfiguration _eConfig;

        public MailKitEEmailService(EmailServerConfiguration config)
        {
            _eConfig = config;
        }
        public void Send(EmailMessage msg)
        {
            var message = new MimeMessage();

            message.To.AddRange(msg.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(msg.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = msg.Subject;

            message.Body = new TextPart("Plain")
            {
                Text = msg.Content
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_eConfig.SmtpServer, _eConfig.SmtpPort);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_eConfig.SmtpUsername, _eConfig.SmtpPassword);

                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}
