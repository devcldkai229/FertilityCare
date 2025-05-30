using FertilityCare.Infrastructure.Configurations;
using FertilityCare.UseCase.Events.Registries;
using FertilityCare.UseCase.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailConfiguration _settings;

        public EmailService(IOptions<EmailConfiguration> options)
        {
            _settings = options.Value;
        }

        public async Task SendAsync(ReceiverContent content)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                Subject = content.Subject,
                Body = content.Body,
                IsBodyHtml = content.IsHtml
            };

            mailMessage.To.Add(content.To);

            using var smtp = new SmtpClient(_settings.SmtpServer, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };

             await smtp.SendMailAsync(mailMessage);  
        }
    }
}
